
using System.Security.Cryptography;
using Application.Mapeadores;
using Application.Mapeadores.CatalogoComponentesCurriculares;
using Application.Validacao;
using BNCC.Sync.Agente;
using Domain.Mensagens;
using DotNetEnv;
using Infrastructure.Ollama;
using Infrastructure.Json.Models;
using Infrastructure.Persistence;
using Infrastructure.RabbitMq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

Env.Load();

const int paginasPorLote = 5;

const string urlPdf =
    "https://basenacionalcomum.mec.gov.br/images/BNCC_EI_EF_110518_versaofinal_site.pdf";

const string pastaDados =
    "/home/danilo/Área de trabalho/Backup/SGED/SGED/data/bncc";

Directory.CreateDirectory(pastaDados);

var caminhoPdf = Path.Combine(pastaDados, "bncc.pdf");
var caminhoJsonFinal = Path.Combine(pastaDados, "current.json");

using var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

var connectionString =
    Environment.GetEnvironmentVariable("BNCC_DATABASE_CONNECTION")
    ?? throw new InvalidOperationException(
        "A variável BNCC_DATABASE_CONNECTION não foi configurada.");

var opcoesDb = new DbContextOptionsBuilder<BnccSyncDbContext>()
    .UseNpgsql(connectionString)
    .Options;

var fabricaDb = new BnccSyncDbContextFactory(opcoesDb);
var controle = new ControleProcessamentoBncc(fabricaDb);

await BaixarPdfSeNecessarioAsync(
    urlPdf,
    caminhoPdf,
    cancellationToken);

var agente = new AgenteBncc(new OllamaOptions());

using var processador = new ProcessadorBncc(
    caminhoPdf,
    pastaDados,
    agente);

await controle.InicializarAsync(
    processador.TotalPaginas,
    paginasPorLote,
    cancellationToken);

await RetomarControleAsync(
    fabricaDb,
    cancellationToken);

var estadoInicial = await controle.ObterAsync(cancellationToken)
    ?? throw new InvalidOperationException(
        "Não foi possível obter o controle do processamento.");

var rabbitOptions = new RabbitMqOptions
{
    Host = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",

    Port = int.TryParse(
        Environment.GetEnvironmentVariable("RABBITMQ_PORT"),
        out var porta)
        ? porta
        : 5672,

    Username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest",
    Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
    VirtualHost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST") ?? "/",
    Queue = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE") ?? "bncc.extracao"
};

await using var rabbit = await RabbitMqConnection.CreateAsync(
    rabbitOptions,
    cancellationToken);

await using var canalPublicacao = await rabbit.CreateChannelAsync(
    rabbitOptions.Queue,
    cancellationToken);

await using var canalConsumo = await rabbit.CreateChannelAsync(
    rabbitOptions.Queue,
    cancellationToken);

var publicador = new RabbitMqPublisher(
    canalPublicacao,
    rabbitOptions.Queue);

async Task FinalizarAsync(CancellationToken ct)
{
    var estado = await controle.ObterAsync(ct)
        ?? throw new InvalidOperationException(
            "Controle não encontrado durante a finalização.");

    if (estado.Status != "Concluido")
        throw new InvalidOperationException(
            $"O processamento ainda não foi concluído. Status: {estado.Status}.");

    var resultados = JsonConvert.DeserializeObject<List<Root>>(
        estado.ResultadoJson ?? "[]") ?? [];

    if (resultados.Count == 0)
        throw new InvalidOperationException(
            "Não existem resultados persistidos para gerar o JSON final.");
    
    var documento = resultados[0];

    documento.Curriculum = resultados
        .SelectMany(r => r.Curriculum ?? [])
        .ToList();
    
    var catalogo = new CatalogoComponentesCurriculares();
    var validador = new ValidadorBncc(catalogo);

    Console.WriteLine("Iniciando validação estrutural da BNCC...");

    var resultadoValidacao = validador.Validar(documento);

    if (!resultadoValidacao.Valido)
    {
        Console.Error.WriteLine(
            $"A validação encontrou {resultadoValidacao.Ocorrencias.Count} ocorrência(s):");

        foreach (var ocorrencia in resultadoValidacao.Ocorrencias)
        {
            Console.Error.WriteLine(
                $"[{ocorrencia.Tipo}] " +
                $"Código: {ocorrencia.Codigo ?? "N/A"} | " +
                $"Localização: {ocorrencia.Localizacao} | " +
                $"Detalhe: {ocorrencia.Mensagem}");
        }

        throw new InvalidOperationException(
            "A BNCC não passou na validação estrutural. " +
            "O processamento não será finalizado.");
    }

    Console.WriteLine("Validação estrutural concluída com sucesso.");

    var mapeador = new MapeadorBncc(catalogo);
    var resultadoMapeamento = mapeador.Mapear(documento);

    Console.WriteLine(
        $"Mapeamento concluído: " +
        $"{resultadoMapeamento.ComponentesCurriculares.Count} componentes, " +
        $"{resultadoMapeamento.UnidadesTematicas.Count} unidades temáticas, " +
        $"{resultadoMapeamento.ObjetosConhecimento.Count} objetos de conhecimento e " +
        $"{resultadoMapeamento.Habilidades.Count} habilidades.");

    var jsonFinal = JsonConvert.SerializeObject(
        documento,
        Formatting.Indented);
    
    var caminhoTemporario = caminhoJsonFinal + ".tmp";

    await File.WriteAllTextAsync(
        caminhoTemporario,
        jsonFinal,
        cancellationToken: ct);

    File.Move(
        caminhoTemporario,
        caminhoJsonFinal,
        overwrite: true);

    await controle.ExcluirControleAsync(ct);

    Console.WriteLine($"JSON final salvo em: {caminhoJsonFinal}");
    Console.WriteLine("Processamento BNCC concluído.");
}

if (estadoInicial.Status == "Concluido")
{
    await FinalizarAsync(cancellationToken);
    return;
}

async Task ProcessarLoteAsync(
    int paginaInicial,
    int paginaFinal,
    CancellationToken ct)
{
    var resultado = await processador.ProcessarLoteAsync(
        paginaInicial,
        paginaFinal,
        ct);

    await controle.RegistrarLoteAsync(
        paginaInicial,
        paginaFinal,
        resultado,
        processador.TotalPaginas,
        paginasPorLote,
        ct);

    Console.WriteLine(
        $"Lote {paginaInicial}-{paginaFinal} persistido no PostgreSQL.");
}

var estadoAtual = await controle.ObterAsync(cancellationToken)
    ?? throw new InvalidOperationException(
        "Controle não encontrado antes do início do consumo.");

await publicador.PublicarAsync(
    new ExtrairLoteBncc(
        estadoAtual.PaginaInicial,
        estadoAtual.PaginaFinal),
    cancellationToken);

var consumidor = new RabbitMqConsumer(
    canalConsumo,
    rabbitOptions.Queue,
    controle,
    ProcessarLoteAsync,
    publicador,
    paginasPorLote,
    FinalizarAsync);

Console.WriteLine(
    $"Iniciando processamento do lote " +
    $"{estadoAtual.PaginaInicial}-{estadoAtual.PaginaFinal}.");

await consumidor.ConsumirAsync(cancellationToken);

static async Task BaixarPdfSeNecessarioAsync(
    string url,
    string caminhoPdf,
    CancellationToken cancellationToken)
{
    using var http = new HttpClient
    {
        Timeout = TimeSpan.FromMinutes(5)
    };

    var caminhoTemporario = caminhoPdf + ".download";

    try
    {
        using var resposta = await http.GetAsync(
            url,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        resposta.EnsureSuccessStatusCode();

        await using (var origem = await resposta.Content.ReadAsStreamAsync(
                         cancellationToken))
        await using (var destino = File.Create(caminhoTemporario))
        {
            await origem.CopyToAsync(destino, cancellationToken);
        }

        if (File.Exists(caminhoPdf))
        {
            var hashAtual = await CalcularSha256Async(
                caminhoPdf,
                cancellationToken);

            var hashNovo = await CalcularSha256Async(
                caminhoTemporario,
                cancellationToken);

            if (string.Equals(
                    hashAtual,
                    hashNovo,
                    StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("O PDF BNCC não sofreu alterações.");
                return;
            }
            
            throw new InvalidOperationException(
                "O PDF oficial mudou em relação ao arquivo local. " +
                "A atualização foi interrompida para não misturar " +
                "resultados de versões diferentes. É necessário reiniciar " +
                "o processamento e o controle de forma coordenada.");
        }

        File.Move(caminhoTemporario, caminhoPdf);
        Console.WriteLine("PDF BNCC baixado e salvo localmente.");
    }
    finally
    {
        if (File.Exists(caminhoTemporario))
            File.Delete(caminhoTemporario);
    }
}

static async Task<string> CalcularSha256Async(
    string caminho,
    CancellationToken cancellationToken)
{
    await using var stream = File.OpenRead(caminho);
    var hash = await SHA256.HashDataAsync(stream, cancellationToken);

    return Convert.ToHexString(hash);
}

static async Task RetomarControleAsync(
    IDbContextFactory<BnccSyncDbContext> fabricaDb,
    CancellationToken cancellationToken)
{
    await using var db = await fabricaDb.CreateDbContextAsync(
        cancellationToken);

    var estado = await db.Processamentos
        .OrderBy(x => x.Id)
        .FirstOrDefaultAsync(cancellationToken);

    if (estado is null || estado.Status == "Concluido")
        return;

    if (estado.Status is "AguardandoCota" or "Erro")
    {
        estado.Status = "Processando";
        estado.Erro = null;

        await db.SaveChangesAsync(cancellationToken);

        Console.WriteLine(
            $"Retomando do intervalo pendente " +
            $"{estado.PaginaInicial}-{estado.PaginaFinal}.");
    }
}
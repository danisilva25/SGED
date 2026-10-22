using BNCC.Sync.Agente;
using DotNetEnv;
using Domain.Mensagens;
using Infrastructure.Gemini;
using Infrastructure.Persistence;
using Infrastructure.RabbitMq;
using Microsoft.EntityFrameworkCore;

Env.Load();

const string urlPdf =
    "https://basenacionalcomum.mec.gov.br/images/BNCC_EI_EF_110518_versaofinal_site.pdf";

const string pastaDados =
    "/home/danilo/Área de trabalho/Backup/SGED/SGED/data/bncc";

var connectionString =
    Environment.GetEnvironmentVariable("BNCC_DATABASE_CONNECTION")
    ?? throw new InvalidOperationException(
        "BNCC_DATABASE_CONNECTION não foi configurada.");

var optionsBuilder = new DbContextOptionsBuilder<BnccSyncDbContext>();

optionsBuilder.UseNpgsql(connectionString);

await using var dbContext = new BnccSyncDbContext(
    optionsBuilder.Options);

await dbContext.Database.CanConnectAsync();

Console.WriteLine("Conexão com PostgreSQL estabelecida.");

const int paginaInicialTeste = 1;
const int paginaFinalTeste = 5;

var caminhoPdf = Path.Combine(pastaDados, "bncc.pdf");

Directory.CreateDirectory(pastaDados);

using var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

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

try
{
    if (!File.Exists(caminhoPdf))
    {
        Console.WriteLine("Baixando PDF da BNCC...");

        using var http = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(5)
        };

        var bytes = await http.GetByteArrayAsync(
            urlPdf,
            cancellationToken);

        await File.WriteAllBytesAsync(
            caminhoPdf,
            bytes,
            cancellationToken);

        Console.WriteLine("Download concluído.");
    }
    
    var agente = new AgenteBncc(new GeminiOptions());

    using var processador = new ProcessadorBncc(
        caminhoPdf,
        pastaDados,
        agente);

    Console.WriteLine(
        $"PDF carregado: {processador.TotalPaginas} páginas.");
    
    await using var connection =
        await RabbitMqConnection.CreateAsync(
            rabbitOptions,
            cancellationToken);

    Console.WriteLine("Conexão com RabbitMQ estabelecida.");
    
    await using (var canalPublicacao =
                 await connection.CreateChannelAsync(
                     rabbitOptions.Queue,
                     cancellationToken))
    {
        var publisher = new RabbitMqPublisher(
            canalPublicacao,
            rabbitOptions.Queue);

        await publisher.PublicarAsync(
            new ExtrairLoteBncc(
                paginaInicialTeste,
                paginaFinalTeste),
            cancellationToken);

        Console.WriteLine(
            $"Lote publicado: páginas {paginaInicialTeste}–{paginaFinalTeste}.");
    }
    
    await using var canalConsumo =
        await connection.CreateChannelAsync(
            rabbitOptions.Queue,
            cancellationToken);

    var consumer = new RabbitMqConsumer(
        canalConsumo,
        rabbitOptions.Queue);

    Console.WriteLine(
        $"Consumindo a fila '{rabbitOptions.Queue}'. Pressione Ctrl+C para sair.");

    await consumer.ConsumirAsync(
        async (mensagem, token) =>
        {
            Console.WriteLine(
                $"Processando páginas {mensagem.PaginaInicial}–{mensagem.PaginaFinal}...");

            await processador.ProcessarLoteAsync(
                mensagem.PaginaInicial,
                mensagem.PaginaFinal,
                token);

            Console.WriteLine(
                $"Lote {mensagem.PaginaInicial}–{mensagem.PaginaFinal} concluído.");
        },
        cancellationToken);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Erro: {ex}");
    throw;
}
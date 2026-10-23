using System.Text;
using Infrastructure.Json.Models;
using Newtonsoft.Json;
using UglyToad.PdfPig;

namespace BNCC.Sync.Agente;

public sealed class ProcessadorBncc : IDisposable
{
    private readonly PdfDocument _pdf;
    private readonly AgenteBncc _agente;
    private readonly string _pastaDados;

    public ProcessadorBncc(
        string caminhoPdf,
        string pastaDados,
        AgenteBncc agente)
    {
        _pdf = PdfDocument.Open(caminhoPdf);
        _pastaDados = pastaDados;
        _agente = agente;

        Directory.CreateDirectory(_pastaDados);
    }

    public int TotalPaginas => _pdf.NumberOfPages;

    public async Task<Root> ProcessarLoteAsync(
        int paginaInicial,
        int paginaFinal,
        CancellationToken cancellationToken = default)
    {
        if (paginaInicial < 1 ||
            paginaFinal < paginaInicial ||
            paginaFinal > TotalPaginas)
        {
            throw new ArgumentOutOfRangeException(
                nameof(paginaInicial),
                $"Intervalo inválido: {paginaInicial}-{paginaFinal}.");
        }

        var conteudo = new StringBuilder();

        for (var numero = paginaInicial;
             numero <= paginaFinal;
             numero++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var pagina = _pdf.GetPage(numero);

            conteudo.AppendLine($"[PÁGINA {numero}]");
            conteudo.AppendLine(pagina.Text);
            conteudo.AppendLine();
        }

        var resultado = await _agente.ExtrairAsync(
            conteudo.ToString(),
            cancellationToken);

        if (resultado is null)
            throw new InvalidOperationException(
                $"O agente retornou resultado nulo no lote " +
                $"{paginaInicial}-{paginaFinal}.");

        var caminhoParcial = Path.Combine(
            _pastaDados,
            $"parcial_{paginaInicial}_{paginaFinal}.json");

        var json = JsonConvert.SerializeObject(
            resultado,
            Formatting.Indented);

        await File.WriteAllTextAsync(
            caminhoParcial,
            json,
            cancellationToken);

        Console.WriteLine(
            $"Lote {paginaInicial}-{paginaFinal} processado.");

        return resultado;
    }

    public void Dispose() 
        => _pdf.Dispose();
}
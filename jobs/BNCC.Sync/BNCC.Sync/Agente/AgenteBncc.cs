
using System.Net.Http.Json;
using System.Text.Json;
using BNCC.Sync.Agente;
using Infrastructure.Json.Models;
using Infrastructure.Ollama;

namespace BNCC.Sync.Agente;

public sealed class AgenteBncc(OllamaOptions options) : IAgenteBncc
{
    private const int MinCaracteresUteis = 300;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<Root> ExtrairAsync(
        string conteudoFonte,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(conteudoFonte))
        {
            throw new ArgumentException(
                "O conteúdo da BNCC não pode ser vazio.",
                nameof(conteudoFonte));
        }

        if (conteudoFonte.Trim().Length < MinCaracteresUteis)
        {
            Console.WriteLine(
                $"Lote com apenas {conteudoFonte.Trim().Length} caracteres. " +
                "Nenhuma chamada ao Ollama será realizada.");

            return new Root { Curriculum = [] };
        }

        var request = new
        {
            model = options.Model,
            stream = false,
            think = false,
            format = SchemaBncc.Criar(),
            options = new
            {
                temperature = 0
            },
            messages = new[]
            {
                new
                {
                    role = "system",
                    content = PromptBncc.Sistema
                },
                new
                {
                    role = "user",
                    content = $"""
                        Extraia os dados curriculares do conteúdo abaixo.

                        Se não houver conteúdo curricular, retorne
                        "curriculum" como lista vazia.

                        Não invente informações e preserve os códigos,
                        as descrições e a hierarquia presentes na fonte.

                        CONTEÚDO DA FONTE:

                        {conteudoFonte}
                        """
                }
            }
        };

        var url = $"{options.BaseUrl.TrimEnd('/')}/api/chat";

        using var response = await new HttpClient().PostAsJsonAsync(
            url,
            request,
            JsonOptions,
            cancellationToken);

        var corpoResposta = await response.Content.ReadAsStringAsync(
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"O Ollama retornou HTTP {(int)response.StatusCode}. " +
                $"Resposta: {corpoResposta}");
        }

        try
        {
            using var documentoResposta = JsonDocument.Parse(corpoResposta);
            var raiz = documentoResposta.RootElement;

            if (raiz.TryGetProperty("done_reason", out var motivo) &&
                motivo.GetString() == "length")
            {
                throw new InvalidOperationException(
                    "O Ollama atingiu o limite de saída. " +
                    "Reduza o tamanho do lote.");
            }

            if (!raiz.TryGetProperty("message", out var mensagem) ||
                !mensagem.TryGetProperty("content", out var conteudo) ||
                string.IsNullOrWhiteSpace(conteudo.GetString()))
            {
                throw new InvalidOperationException(
                    "O Ollama não retornou conteúdo textual.");
            }

            var json = conteudo.GetString()!;

            return JsonSerializer.Deserialize<Root>(json, JsonOptions)
                ?? throw new InvalidOperationException(
                    "O JSON do Ollama não pôde ser convertido em Root.");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                "O Ollama retornou uma resposta JSON inválida. " +
                $"Tamanho da resposta: {corpoResposta.Length} caracteres.",
                ex);
        }
    }
}

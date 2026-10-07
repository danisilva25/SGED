using System.Text.Json;
using Google.GenAI;
using Google.GenAI.Types;
using Infrastructure.Gemini;
using Infrastructure.Json.Models;

namespace BNCC.Sync.Agente;

public sealed class AgenteBncc(GeminiOptions options) : IAgenteBncc
{
    private readonly Client _client = new(
        apiKey: options.ApiKey);

    public async Task<Root> ExtrairAsync(
        string conteudo,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
        {
            throw new ArgumentException(
                "O conteúdo da BNCC não pode ser vazio.",
                nameof(conteudo));
        }

        var prompt = $"""
            {PromptBncc.Sistema}

            Extraia os dados curriculares do conteúdo abaixo.

            CONTEÚDO DA FONTE:

            {conteudo}
            """;

        var config = new GenerateContentConfig
        {
            SystemInstruction = new Content
            {
                Parts =
                [
                    new Part
                    {
                        Text = PromptBncc.Sistema
                    }
                ]
            },

            Temperature = 0,

            ResponseMimeType = "application/json",

            ResponseJsonSchema = SchemaBncc.Criar()
        };

        var response =
            await _client.Models.GenerateContentAsync(
                model: options.Model,
                contents: conteudo,
                config: config,
                cancellationToken: cancellationToken);

        var json = ExtrairTexto(response);

        var documento =
            JsonSerializer.Deserialize<Root>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        return documento
            ?? throw new InvalidOperationException(
                "O Gemini retornou um JSON que não pôde ser convertido em DocumentoBnccJson.");
    }

    private static string ExtrairTexto(
        GenerateContentResponse response)
    {
        var texto = response
            .Candidates?
            .FirstOrDefault()?
            .Content?
            .Parts?
            .FirstOrDefault()?
            .Text;

        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new InvalidOperationException(
                "O Gemini não retornou conteúdo textual.");
        }

        return texto;
    }
}
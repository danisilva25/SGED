
using System.Text.Json;
using Google.GenAI;
using Google.GenAI.Types;
using Infrastructure.Gemini;
using Infrastructure.Json.Models;

namespace BNCC.Sync.Agente;

public sealed class AgenteBnccGemini : IAgenteBncc
{
    private const int MinCaracteresUteis = 300;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly Client _client;
    private readonly GeminiOptions _options;

    public AgenteBnccGemini(GeminiOptions options)
    {
        _options = options;

        if (string.IsNullOrWhiteSpace(options.ApiKey))
            throw new InvalidOperationException(
                "A variável GEMINI_API_KEY não foi configurada.");

        _client = new Client(
            apiKey: options.ApiKey,
            clientOptions: new ClientOptions
            {
                HttpClientFactory = () => new HttpClient
                {
                    Timeout = TimeSpan.FromMinutes(4)
                }
            });
    }

    public async Task<Root> ExtrairAsync(
        string conteudoFonte,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(conteudoFonte))
            throw new ArgumentException(
                "O conteúdo da BNCC não pode ser vazio.",
                nameof(conteudoFonte));

        if (conteudoFonte.Trim().Length < MinCaracteresUteis)
        {
            Console.WriteLine(
                "Conteúdo insuficiente. Nenhuma chamada ao Gemini será realizada.");

            return new Root { Curriculum = [] };
        }

        var prompt = $"""
            Extraia os dados curriculares do conteúdo abaixo.

            Se não houver conteúdo curricular, retorne
            "curriculum" como lista vazia.

            Não invente informações e preserve os códigos,
            as descrições e a hierarquia presentes na fonte.

            CONTEÚDO DA FONTE:

            {conteudoFonte}
            """;

        var config = new GenerateContentConfig
        {
            Temperature = 0,
            MaxOutputTokens = 16384,
            ResponseMimeType = "application/json",
            ResponseJsonSchema = SchemaBncc.Criar(),
            SystemInstruction = new Content
            {
                Parts =
                [
                    new Part { Text = PromptBncc.Sistema }
                ]
            }
        };

        var resposta = await _client.Models.GenerateContentAsync(
            model: _options.Model,
            contents: prompt,
            config: config,
            cancellationToken: cancellationToken);

        var json = string.Concat(
            resposta.Candidates?
                .SelectMany(c => c.Content?.Parts ?? [])
                .Where(p => !string.IsNullOrWhiteSpace(p.Text))
                .Select(p => p.Text)
            ?? []);

        if (string.IsNullOrWhiteSpace(json))
            throw new InvalidOperationException(
                "O Gemini não retornou conteúdo textual.");

        try
        {
            return JsonSerializer.Deserialize<Root>(
                       json,
                       JsonOptions)
                   ?? throw new InvalidOperationException(
                       "O JSON do Gemini não pôde ser convertido em Root.");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                $"O Gemini retornou JSON inválido. " +
                $"Tamanho: {json.Length} caracteres.",
                ex);
        }
    }
}

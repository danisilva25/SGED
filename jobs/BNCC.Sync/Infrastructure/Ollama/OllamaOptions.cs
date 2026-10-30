namespace Infrastructure.Ollama;

public sealed class OllamaOptions
{
    public string BaseUrl { get; set; } =
        Environment.GetEnvironmentVariable("OLLAMA_BASE_URL")
        ?? "http://localhost:11434";

    public string Model { get; set; } =
        Environment.GetEnvironmentVariable("OLLAMA_MODEL")
        ?? "qwen3:4b";
}
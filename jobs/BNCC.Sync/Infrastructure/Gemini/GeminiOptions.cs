namespace Infrastructure.Gemini;

public class GeminiOptions
{
    public string ApiKey { get; set; } = Environment.GetEnvironmentVariable("GEMINI_API_KEY")!;

    public string Model { get; set; } = "gemini-flash-latest";
}
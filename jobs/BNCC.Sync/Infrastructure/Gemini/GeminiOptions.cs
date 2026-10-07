namespace Infrastructure.Gemini;

public class GeminiOptions
{
    public string ApiKey { get; set; } = string.Empty;

    public string Model { get; set; } = "gemini-flash-latest";
}
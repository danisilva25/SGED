using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class Source
{
    [JsonProperty("name")]
    public string? Name { get; set; }
    
    [JsonProperty("document")]
    public string? Document { get; set; }
    
    [JsonProperty("version")]
    public string? Version { get; set; }
    
    [JsonProperty("file")]
    public string? File { get; set; }
}
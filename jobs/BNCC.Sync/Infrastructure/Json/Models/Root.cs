using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class Root
{
    [JsonProperty("source")]
    public Source? Source { get; set; }
    
    [JsonProperty("curriculum")]
    public List<Curriculum> Curriculum { get; set; } = [];
}
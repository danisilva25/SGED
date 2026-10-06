using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class ComponenteCurricular
{
    [JsonProperty("nome")]
    public string? Nome { get; set; }
}
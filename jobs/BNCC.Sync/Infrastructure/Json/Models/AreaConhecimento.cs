using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class AreaConhecimento
{
    [JsonProperty("nome")]
    public string? Nome { get; set; }
}
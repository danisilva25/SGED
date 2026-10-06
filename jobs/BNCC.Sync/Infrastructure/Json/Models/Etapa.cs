using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class Etapa
{
    [JsonProperty("nome")]
    public string? Nome { get; set; }
}
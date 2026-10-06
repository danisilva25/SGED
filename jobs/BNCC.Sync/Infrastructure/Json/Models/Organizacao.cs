using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class Organizacao
{
    [JsonProperty("tipo")]
    public string? Tipo { get; set; }
    
    [JsonProperty("nome")]
    public string? Nome { get; set; }
}
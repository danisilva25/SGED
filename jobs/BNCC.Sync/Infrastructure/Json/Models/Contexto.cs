using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class Contexto
{
    [JsonProperty("tipo")] 
    public string? Tipo { get; set; }

    [JsonProperty("nome")] 
    public string? Nome { get; set; }
}
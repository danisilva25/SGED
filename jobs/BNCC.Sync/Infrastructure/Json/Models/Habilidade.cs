using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class Habilidade
{
    [JsonProperty("codigo")]
    public string? Codigo { get; set; }
    
    [JsonProperty("descricao")]
    public string? Descricao { get; set; }
    
    [JsonProperty("anos")]
    public List<int> Anos { get; set; } = [];
    
    [JsonProperty("fonte")]
    public Fonte? Fonte { get; set; }
}
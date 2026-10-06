using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class ObjetosConhecimento
{
    [JsonProperty("nome")]
    public string? Nome { get; set; }
    
    [JsonProperty("habilidades")]
    public List<Habilidade> Habilidades { get; set; } = [];
}
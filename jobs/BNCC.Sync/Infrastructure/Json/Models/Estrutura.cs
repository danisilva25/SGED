using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class Estrutura
{
    [JsonProperty("tipo")]
    public string? Tipo { get; set; }
    
    [JsonProperty("nome")]
    public string? Nome { get; set; }
    
    [JsonProperty("contexto")]
    public Contexto? Contexto { get; set; }

    [JsonProperty("objetosConhecimento")] 
    public List<ObjetosConhecimento> ObjetosConhecimento { get; set; } = [];
}
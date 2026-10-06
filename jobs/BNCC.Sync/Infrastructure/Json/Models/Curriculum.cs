using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class Curriculum
{
    [JsonProperty("etapa")]
    public Etapa? Etapa { get; set; }
    
    [JsonProperty("areaConhecimento")]
    public AreaConhecimento? AreaConhecimento { get; set; }
    
    [JsonProperty("componenteCurricular")]
    public ComponenteCurricular? ComponenteCurricular { get; set; }
    
    [JsonProperty("organizacao")]
    public Organizacao? Organizacao { get; set; }
    
    [JsonProperty("estrutura")]
    public List<Estrutura> Estrutura { get; set; } = [];
}
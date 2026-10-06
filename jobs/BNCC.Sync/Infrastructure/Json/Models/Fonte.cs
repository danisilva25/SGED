using Newtonsoft.Json;

namespace Infrastructure.Json.Models;

public class Fonte
{
    [JsonProperty("pagina")]
    public int Pagina { get; set; }
}
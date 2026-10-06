using Infrastructure.Json.Models;
using Newtonsoft.Json;

Console.WriteLine("Hello World");

var file = await File.ReadAllTextAsync(@"/media/danilo/Dados/Backup/SGED/SGED/data/bncc/current.json");

var json = JsonConvert.DeserializeObject<Root>(file);

var a = json;
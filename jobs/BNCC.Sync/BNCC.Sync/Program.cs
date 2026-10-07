using Application.Mapeadores;
using Application.Mapeadores.CatalogoComponentesCurriculares;
using Application.Validacao;
using Infrastructure.Json.Models;
using Newtonsoft.Json;

Console.WriteLine("Hello World");

var file = await File.ReadAllTextAsync(@"/media/danilo/Dados/Backup/SGED/SGED/data/bncc/current.json");

var json = JsonConvert.DeserializeObject<Root>(file);

var catalogoComponente = new CatalogoComponentesCurriculares();

var mapper = new MapeadorBncc(catalogoComponente);

var resultado = new ValidadorBncc(catalogoComponente).Validar(json);

var a = mapper.Mapear(json);

Console.WriteLine(a);
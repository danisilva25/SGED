using Domain.Curriculo.ComponenteCurricular;
using Domain.Curriculo.UnidadeTematica;
using Domain.Curriculo.Habilidade;
using Domain.Curriculo.ObjetoConhecimento;

namespace Application.Mapeadores;

public sealed class ResultadoMapeamentoBncc
{
    public List<ComponenteCurricular> ComponentesCurriculares { get; set; } = [];
    public List<UnidadeTematica> UnidadesTematicas { get; set; } = [];
    public List<ObjetoConhecimento> ObjetosConhecimento { get; set; } = [];
    public List<Habilidade> Habilidades { get; set; } = [];
}
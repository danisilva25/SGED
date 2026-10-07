using Domain.Curriculo.ObjetoConhecimento;
using Domain.Curriculo.UnidadeTematica;
using Infrastructure.Json.Models;
using ComponenteCurricular = Domain.Curriculo.ComponenteCurricular.ComponenteCurricular;
using Habilidade = Domain.Curriculo.Habilidade.Habilidade;

namespace Application.Mapeadores;

public sealed class MapeadorBncc(CatalogoComponentesCurriculares.CatalogoComponentesCurriculares catalogoSiglas)
{
    private readonly CatalogoComponentesCurriculares.CatalogoComponentesCurriculares CatalogoSiglas = catalogoSiglas;

    public ResultadoMapeamentoBncc Mapear(
        Root documento)
    {
        var resultado = new ResultadoMapeamentoBncc();

        foreach (var curriculo in documento.Curriculum)
        {
            var componenteCurricular = MapearComponente(curriculo);

            resultado.ComponentesCurriculares.Add(
                componenteCurricular);

            foreach (var estrutura in curriculo.Estrutura)
            {
                var unidadeTematica = MapearUnidadeTematica(
                    estrutura,
                    componenteCurricular.Id);

                resultado.UnidadesTematicas.Add(
                    unidadeTematica);

                foreach (var objetoJson in estrutura.ObjetosConhecimento)
                {
                    var objetoConhecimento = MapearObjetoConhecimento(
                        objetoJson,
                        unidadeTematica.Id);

                    resultado.ObjetosConhecimento.Add(
                        objetoConhecimento);

                    foreach (var habilidadeJson in objetoJson.Habilidades)
                    {
                        var habilidade = MapearHabilidade(
                            habilidadeJson,
                            objetoConhecimento.Id);

                        resultado.Habilidades.Add(
                            habilidade);
                    }
                }
            }
        }

        return resultado;
    }

    private ComponenteCurricular MapearComponente(
        Curriculum curriculo)
    {
        return new ComponenteCurricular(
            nome: curriculo?.ComponenteCurricular?.Nome ?? "",
            sigla: CatalogoSiglas.Obter(curriculo?.ComponenteCurricular?.Nome ?? ""),
            areaConhecimento: curriculo?.AreaConhecimento?.Nome ?? "");
    }

    private static UnidadeTematica MapearUnidadeTematica(
        Estrutura estrutura,
        Guid componenteCurricularId)
    {
        return new UnidadeTematica(
            componenteCurricularId,
            estrutura!.Nome!);
    }

    private static ObjetoConhecimento MapearObjetoConhecimento(
        ObjetosConhecimento objetoJson,
        Guid unidadeTematicaId)
    {
        return new ObjetoConhecimento(
            unidadeTematicaId,
            objetoJson.Nome!);
    }

    private static Habilidade MapearHabilidade(
        Infrastructure.Json.Models.Habilidade habilidadeJson,
        Guid objetoConhecimentoId)
    {
        return new Habilidade(
            habilidadeJson!.Codigo!,
            habilidadeJson!.Descricao!,
            objetoConhecimentoId);
    }
}
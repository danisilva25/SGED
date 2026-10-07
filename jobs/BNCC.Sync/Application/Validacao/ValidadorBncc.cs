using Application.Mapeadores.CatalogoComponentesCurriculares;
using Infrastructure.Json.Models;

namespace Application.Validacao;

public sealed class ValidadorBncc(CatalogoComponentesCurriculares catalogoSiglas)
{
    public ResultadoValidacaoBncc Validar(
        Root? documento)
    {
        var resultado = new ResultadoValidacaoBncc();

        if (documento is null)
        {
            resultado.Erros.Add(
                "O documento BNCC não pode ser nulo.");

            return resultado;
        }

        ValidarSource(documento, resultado);
        ValidarCurriculum(documento, resultado);

        return resultado;
    }

    private static void ValidarSource(
        Root documento,
        ResultadoValidacaoBncc resultado)
    {
        if (documento.Source is null)
        {
            resultado.Erros.Add(
                "O campo 'source' deve existir.");

            return;
        }

        if (string.IsNullOrWhiteSpace(documento.Source.Name))
            resultado.Erros.Add(
                "O campo 'source.name' deve possuir valor.");

        if (string.IsNullOrWhiteSpace(documento.Source.Document))
            resultado.Erros.Add(
                "O campo 'source.document' deve possuir valor.");

        if (string.IsNullOrWhiteSpace(documento.Source.Version))
            resultado.Erros.Add(
                "O campo 'source.version' deve possuir valor.");

        if (string.IsNullOrWhiteSpace(documento.Source.File))
            resultado.Erros.Add(
                "O campo 'source.file' deve possuir valor.");
    }

    private void ValidarCurriculum(
        Root documento,
        ResultadoValidacaoBncc resultado)
    {
        if (documento.Curriculum is null ||
            documento.Curriculum.Count == 0)
        {
            resultado.Erros.Add(
                "O campo 'curriculum' deve existir e possuir pelo menos um registro.");

            return;
        }

        for (var i = 0; i < documento.Curriculum.Count; i++)
        {
            var curriculo = documento.Curriculum[i];

            ValidarEtapa(curriculo, i, resultado);
            ValidarComponente(curriculo, i, resultado);
            ValidarOrganizacao(curriculo, i, resultado);
            ValidarEstrutura(curriculo, i, resultado);
        }
    }

    private static void ValidarEtapa(
        Curriculum curriculo,
        int indice,
        ResultadoValidacaoBncc resultado)
    {
        if (curriculo.Etapa is null)
        {
            resultado.Erros.Add(
                $"curriculum[{indice}].etapa deve existir.");

            return;
        }

        if (string.IsNullOrWhiteSpace(curriculo.Etapa.Nome))
        {
            resultado.Erros.Add(
                $"curriculum[{indice}].etapa.nome deve possuir valor.");
        }
    }

    private void ValidarComponente(
        Curriculum curriculo,
        int indice,
        ResultadoValidacaoBncc resultado)
    {
        if (curriculo.ComponenteCurricular is null)
        {
            resultado.Erros.Add(
                $"curriculum[{indice}].componenteCurricular deve existir.");

            return;
        }

        var nome = curriculo.ComponenteCurricular.Nome;

        if (string.IsNullOrWhiteSpace(nome))
        {
            resultado.Erros.Add(
                $"curriculum[{indice}].componenteCurricular.nome deve possuir valor.");

            return;
        }

        if (!catalogoSiglas.Possui(nome))
            resultado.Erros.Add(
                $"O componente curricular '{nome}' não possui sigla cadastrada.");
        
    }

    private static void ValidarOrganizacao(
        Curriculum curriculo,
        int indice,
        ResultadoValidacaoBncc resultado)
    {
        if (curriculo.Organizacao is null)
        {
            resultado.Erros.Add(
                $"curriculum[{indice}].organizacao deve existir.");

            return;
        }

        if (string.IsNullOrWhiteSpace(curriculo.Organizacao.Tipo))
            resultado.Erros.Add(
                $"curriculum[{indice}].organizacao.tipo deve possuir valor.");
        
    }

    private static void ValidarEstrutura(
        Curriculum curriculo,
        int indiceCurriculo,
        ResultadoValidacaoBncc resultado)
    {
        if (curriculo.Estrutura is null ||
            curriculo.Estrutura.Count == 0)
        {
            resultado.Erros.Add(
                $"curriculum[{indiceCurriculo}].estrutura deve existir e possuir registros.");

            return;
        }

        for (var i = 0; i < curriculo.Estrutura.Count; i++)
        {
            var estrutura = curriculo.Estrutura[i];

            if (string.IsNullOrWhiteSpace(estrutura.Nome))
                resultado.Erros.Add(
                    $"curriculum[{indiceCurriculo}].estrutura[{i}].nome deve possuir valor.");

            if (estrutura.ObjetosConhecimento is null ||
                estrutura.ObjetosConhecimento.Count == 0)
            {
                resultado.Erros.Add(
                    $"curriculum[{indiceCurriculo}].estrutura[{i}].objetosConhecimento deve possuir pelo menos um objeto.");
                
                continue;
            }

            ValidarObjetosConhecimento(
                estrutura.ObjetosConhecimento,
                indiceCurriculo,
                i,
                resultado);
        }
    }

    private static void ValidarObjetosConhecimento(
        List<ObjetosConhecimento> objetos,
        int indiceCurriculo,
        int indiceEstrutura,
        ResultadoValidacaoBncc resultado)
    {
        for (var i = 0; i < objetos.Count; i++)
        {
            var objeto = objetos[i];

            if (string.IsNullOrWhiteSpace(objeto.Nome))
            {
                resultado.Erros.Add(
                    $"curriculum[{indiceCurriculo}].estrutura[{indiceEstrutura}].objetosConhecimento[{i}].nome deve possuir valor.");
            }

            if (objeto.Habilidades is null ||
                objeto.Habilidades.Count == 0)
            {
                resultado.Erros.Add(
                    $"curriculum[{indiceCurriculo}].estrutura[{indiceEstrutura}].objetosConhecimento[{i}].habilidades deve possuir pelo menos uma habilidade.");

                continue;
            }

            ValidarHabilidades(
                objeto.Habilidades,
                indiceCurriculo,
                indiceEstrutura,
                i,
                resultado);
        }
    }

    private static void ValidarHabilidades(
        List<Infrastructure.Json.Models.Habilidade> habilidades,
        int indiceCurriculo,
        int indiceEstrutura,
        int indiceObjeto,
        ResultadoValidacaoBncc resultado)
    {
        for (var i = 0; i < habilidades.Count; i++)
        {
            var habilidade = habilidades[i];

            if (string.IsNullOrWhiteSpace(habilidade.Codigo))
                resultado.Erros.Add(
                    $"curriculum[{indiceCurriculo}].estrutura[{indiceEstrutura}].objetosConhecimento[{indiceObjeto}].habilidades[{i}].codigo deve possuir valor.");

            if (string.IsNullOrWhiteSpace(habilidade.Descricao))
                resultado.Erros.Add(
                    $"curriculum[{indiceCurriculo}].estrutura[{indiceEstrutura}].objetosConhecimento[{indiceObjeto}].habilidades[{i}].descricao deve possuir valor.");

            if (habilidade.Anos is null ||
                habilidade.Anos.Count == 0)
                resultado.Erros.Add(
                    $"A habilidade '{habilidade.Codigo}' deve possuir pelo menos um ano.");
        }
    }
}
using System.Text.RegularExpressions;
using Application.Mapeadores.CatalogoComponentesCurriculares;
using Infrastructure.Json.Models;

namespace Application.Validacao;

public sealed class ValidadorBncc(CatalogoComponentesCurriculares catalogoSiglas)
{
    public ResultadoValidacaoBncc Validar(
        Root documento,
        IReadOnlyDictionary<string, ReferenciaHabilidade>? referencia = null,
        IReadOnlyCollection<string>? codigosEsperadosNoLote = null)
    {
        ArgumentNullException.ThrowIfNull(documento);

        var ocorrencias = new List<OcorrenciaValidacaoBncc>();
        var encontrados = new Dictionary<string, string>(
            StringComparer.OrdinalIgnoreCase);

        foreach (var curriculo in documento.Curriculum)
        {
            foreach (var estrutura in curriculo.Estrutura)
            {
                foreach (var objeto in estrutura.ObjetosConhecimento)
                {
                    var localizacao = string.Join(
                        " > ",
                        new[]
                        {
                            curriculo.Etapa?.Nome,
                            curriculo.ComponenteCurricular?.Nome,
                            estrutura.Nome,
                            objeto.Nome
                        }.Where(x => !string.IsNullOrWhiteSpace(x)));

                    foreach (var habilidade in objeto.Habilidades)
                    {
                        var codigo = habilidade.Codigo?.Trim();
                        var descricao = habilidade.Descricao?.Trim();

                        if (string.IsNullOrWhiteSpace(codigo))
                        {
                            ocorrencias.Add(new(
                                TipoOcorrenciaBncc.CodigoVazio,
                                "Habilidade sem código.",
                                null,
                                localizacao));

                            continue;
                        }

                        if (encontrados.TryGetValue(
                                codigo, out var primeiraLocalizacao))
                        {
                            ocorrencias.Add(new(
                                TipoOcorrenciaBncc.CodigoDuplicado,
                                $"Código repetido. Primeira ocorrência: {primeiraLocalizacao}.",
                                codigo,
                                localizacao));
                        }
                        else
                        {
                            encontrados[codigo] = localizacao;
                        }

                        if (referencia is null)
                            continue;

                        if (!referencia.TryGetValue(
                                codigo, out var habilidadeOficial))
                        {
                            ocorrencias.Add(new(
                                TipoOcorrenciaBncc.CodigoDesconhecido,
                                "Código não encontrado na referência fornecida.",
                                codigo,
                                localizacao));

                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(descricao) ||
                            Normalizar(descricao) !=
                            Normalizar(habilidadeOficial.Descricao))
                        {
                            ocorrencias.Add(new(
                                TipoOcorrenciaBncc.DescricaoDivergente,
                                "A descrição difere da referência fornecida.",
                                codigo,
                                localizacao));
                        }
                    }
                }
            }
        }

        if (codigosEsperadosNoLote is not null)
        {
            var esperados = codigosEsperadosNoLote
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Select(c => c.Trim())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var codigoEsperado in esperados)
            {
                if (!encontrados.ContainsKey(codigoEsperado))
                {
                    ocorrencias.Add(new(
                        TipoOcorrenciaBncc.HabilidadeAusente,
                        "Habilidade esperada não encontrada no lote.",
                        codigoEsperado,
                        "Lote"));
                }
            }
        }

        return new ResultadoValidacaoBncc(ocorrencias);
    }

    private static string Normalizar(string texto) =>
        Regex.Replace(texto.Trim(), @"\s+", " ");
}
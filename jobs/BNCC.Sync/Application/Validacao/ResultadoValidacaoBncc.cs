namespace Application.Validacao;

public sealed class ResultadoValidacaoBncc
{
    public IReadOnlyList<OcorrenciaValidacaoBncc> Ocorrencias { get; }

    public bool Valido => Ocorrencias.Count == 0;

    public ResultadoValidacaoBncc(
        IReadOnlyList<OcorrenciaValidacaoBncc> ocorrencias)
    {
        Ocorrencias = ocorrencias;
    }
}
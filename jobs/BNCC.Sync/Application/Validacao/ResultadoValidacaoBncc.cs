namespace Application.Validacao;

public sealed class ResultadoValidacaoBncc
{
    public bool EhValido => Erros.Count == 0;

    public List<string> Erros { get; } = [];
}
namespace Domain.Curriculo.Habilidade;

public class Habilidade
{
    public Guid Id { get; private set; }

    public string CodigoBNCC { get; private set; } = string.Empty;

    public string Descricao { get; private set; } = string.Empty;

    public Guid ObjetoConhecimentoId { get; private set; }

    private Habilidade()
    {
    }

    public Habilidade(
        string codigoBNCC,
        string descricao,
        Guid objetoConhecimentoId)
    {
        Id = Guid.NewGuid();
        CodigoBNCC = codigoBNCC;
        Descricao = descricao;
        ObjetoConhecimentoId = objetoConhecimentoId;
    }
}
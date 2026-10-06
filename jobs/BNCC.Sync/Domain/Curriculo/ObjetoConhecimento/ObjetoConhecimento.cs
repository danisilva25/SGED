namespace Domain.Curriculo.ObjetoConhecimento;

public class ObjetoConhecimento
{
    public Guid Id { get; private set; }

    public Guid UnidadeTematicaId { get; private set; }

    public string Descricao { get; private set; } = string.Empty;

    private ObjetoConhecimento()
    {
    }

    public ObjetoConhecimento(
        Guid unidadeTematicaId,
        string descricao)
    {
        Id = Guid.NewGuid();
        UnidadeTematicaId = unidadeTematicaId;
        Descricao = descricao;
    }
}
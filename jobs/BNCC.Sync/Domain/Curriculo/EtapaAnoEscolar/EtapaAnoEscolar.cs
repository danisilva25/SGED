namespace Domain.Curriculo.EtapaAnoEscolar;

public class EtapaAnoEscolar
{
    public string? Nome { get; private set; }
    public int? Codigo { get; private set; }
    public Guid ModalidadeId { get; private set; }
    public int? Ordem { get; private set; }
    
    private EtapaAnoEscolar(){}

    public EtapaAnoEscolar(string nome, int codigo, Guid modalidadeId, int ordem)
    {
        Nome = nome;
        Codigo = codigo;
        ModalidadeId = modalidadeId;
        Ordem = ordem;
    }
}
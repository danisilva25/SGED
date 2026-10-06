namespace Domain.Curriculo.MatrizCurricular;

public class MatrizCurricular
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public Guid AnoLetivoId { get; private set; }

    public Guid EtapaAnoEscolarId { get; private set; }

    public int CargaHorariaTotal { get; private set; }

    private MatrizCurricular()
    {
    }

    public MatrizCurricular(
        string nome,
        Guid anoLetivoId,
        Guid etapaAnoEscolarId,
        int cargaHorariaTotal)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        AnoLetivoId = anoLetivoId;
        EtapaAnoEscolarId = etapaAnoEscolarId;
        CargaHorariaTotal = cargaHorariaTotal;
    }
}
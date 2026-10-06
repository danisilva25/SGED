namespace Domain.Curriculo.ItemMatrizCurricular;

public class ItemMatrizCurricular
{
    public Guid Id { get; private set; }

    public Guid MatrizCurricularId { get; private set; }

    public Guid ComponenteCurricularId { get; private set; }

    public int QuantidadeAulasSemanais { get; private set; }

    public int CargaHoraria { get; private set; }

    public string Periodo { get; private set; } = string.Empty;

    private ItemMatrizCurricular()
    {
    }

    public ItemMatrizCurricular(
        Guid matrizCurricularId,
        Guid componenteCurricularId,
        int quantidadeAulasSemanais,
        int cargaHoraria,
        string periodo)
    {
        Id = Guid.NewGuid();
        MatrizCurricularId = matrizCurricularId;
        ComponenteCurricularId = componenteCurricularId;
        QuantidadeAulasSemanais = quantidadeAulasSemanais;
        CargaHoraria = cargaHoraria;
        Periodo = periodo;
    }
}
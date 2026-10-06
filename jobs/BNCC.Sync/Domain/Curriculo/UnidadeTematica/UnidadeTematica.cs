namespace Domain.Curriculo.UnidadeTematica;

public class UnidadeTematica
{
    public Guid Id { get; private set; }
    public Guid ComponenteCurricularId { get; private set; }
    public string Descricao { get; private set; }
    
    private UnidadeTematica(){}

    public UnidadeTematica(Guid componenteCurricularId, string descricao)
    {
        Id = Guid.NewGuid();
        ComponenteCurricularId = componenteCurricularId;
        Descricao = descricao;
    }
}
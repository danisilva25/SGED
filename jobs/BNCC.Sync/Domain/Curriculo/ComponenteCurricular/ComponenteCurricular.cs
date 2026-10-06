namespace Domain.Curriculo.ComponenteCurricular;

public class ComponenteCurricular
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Sigla { get; private set; }
    public string AreaConhecimento { get; private set; }
    
    private ComponenteCurricular(){}

    public ComponenteCurricular(string nome, string sigla, string areaConhecimento)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Sigla = sigla;
        AreaConhecimento = areaConhecimento;
    }
}
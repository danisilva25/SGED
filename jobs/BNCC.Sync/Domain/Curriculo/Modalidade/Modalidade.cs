namespace Domain.Curriculo.Modalidade;

public class Modalidade
{
    public Guid Id { get; private set; }
    public int Codigo { get; private set; }
    public string? Nome { get; private set; }
    public string? Descricao { get; private set; }
    
    private Modalidade(){}

    public Modalidade(int codigo, string nome, string descricao)
    {
        Id = Guid.NewGuid();
        Codigo = codigo;
        Nome = nome;
        Descricao = descricao;
    }
}
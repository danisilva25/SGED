using SGED.Domain.Common;

namespace SGED.Domain.Curriculo.Disciplinas;

public sealed class Disciplina : Entity
{
    public string Nome { get; private set; }
    
    private Disciplina() { }

    private Disciplina(string nome)
    => Nome = nome;

    public static Disciplina Create(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome da disciplina é obrigatório.");

        return new Disciplina(nome.Trim());
    }
}
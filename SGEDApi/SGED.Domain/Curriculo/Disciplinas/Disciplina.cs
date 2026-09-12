using SGED.Domain.Common;
using SGED.Domain.Common.Results;

namespace SGED.Domain.Curriculo.Disciplinas;

public sealed class Disciplina : Entity
{
    public string Nome { get; private set; }
    
    private Disciplina() { }

    private Disciplina(string nome)
    => Nome = nome;

    public static Result<Disciplina> Create(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return Result<Disciplina>.Failure(
                new Error(
                    "Disciplina.NomeObrigatorio",
                    "O nome da disciplina é obrigatório.",
                    ErrorType.Validation
                )
            );
            
        return Result<Disciplina>.Success(
            new Disciplina(nome.Trim()));
    }
}
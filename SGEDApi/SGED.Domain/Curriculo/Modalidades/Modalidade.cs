using SGED.Domain.Common;
using SGED.Domain.Common.Results;

namespace SGED.Domain.Curriculo.Modalidades;

public sealed class Modalidade : Entity
{
    public int Codigo { get; private set; }

    public string Nome { get; private set; }

    private Modalidade()
    {
    }

    private Modalidade(string nome, int codigo)
    {
        Codigo = codigo;
        Nome = nome;
    }

    private static Result<bool> ValidaModalidade(
        string nome,
        int codigo)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return Result<bool>.Failure(new Error(
                "Modalidade.NomeObrigatorio",
                "O nome é obrigatório",
                ErrorType.Validation
            ));

        if (codigo == 0)
            return Result<bool>.Failure(new Error(
                "Modalidade.CodigoZerado",
                "O código deve ser maior que 0.",
                ErrorType.Validation
            ));

        return Result<bool>.Success(true);
    }

    public static Result<Modalidade> Create(
        string nome,
        int codigo)
    {
        var validation = ValidaModalidade(nome, codigo);
        
        if(validation.IsFailure)
            return Result<Modalidade>.Failure(validation.Error);

        return Result<Modalidade>.Success(
            new Modalidade(nome.Trim(), codigo));
    }

    public Result<bool> Update(
        string nome,
        int codigo)
    {
        var validation = ValidaModalidade(nome, codigo);
        
        if(validation.IsFailure)
            return Result<bool>.Failure(validation.Error);
        
        Nome =  nome.Trim();
        Codigo = codigo;
        
        MarkAsUpdated();
        
        return Result<bool>.Success(true);
    }
}
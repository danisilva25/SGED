namespace SGED.Domain.Common.ValueObjects;

public sealed class Cpf : ValueObject
{
    public string Numero { get; }

    private Cpf(string numero)
    {
        Numero = numero;
    }

    public static Cpf Create(string numero)
    {
        if(string.IsNullOrEmpty(numero))
            throw new DomainException("Cpf não pode ser vazio.");
        
        var cpf = new string(numero.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            throw new DomainException("CPF deve conter 11 caracteres.");
        
        if(TodosDigitoIguais(numero))
            throw new DomainException("CPF inválido.");
        
        var primeiroDigito = CalcularPrimeiroDigito(cpf);

        if (primeiroDigito != cpf[9] - '0')
            throw new DomainException(
                "CPF inválido.");

        var segundoDigito = CalcularSegundoDigito(cpf);

        if (segundoDigito != cpf[10] - '0')
            throw new DomainException(
                "CPF inválido.");

        return new Cpf(cpf);
    }
    private static int CalcularPrimeiroDigito(string cpf)
    {
        var soma = 0;
        
        for(var i =0; i < 9; i++)
            soma += (cpf[i] - '0') * (10 - i);

        var resto = soma % 11;
        
        return resto < 2 ?  0 : 11 - resto;
    }
    private static int CalcularSegundoDigito(string cpf)
    {
        var soma = 0;
        
        for(var i = 0; i < 10; i++)
            soma += (cpf[i] - '0') * (11 - i);
        
        var resto = soma % 11;
        
        return resto < 2 ?  0 : 11 - resto;
    }

    private static bool TodosDigitoIguais(string cpf) =>
        cpf.All(digito => digito == cpf[0]);
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Numero;
    }
}
namespace SGED.Domain.Common.ValueObjects;

public sealed class RA : ValueObject
{
    public string Numero { get; private set; }
    public string DigitoVerificador { get; private set; }
    public string UF { get; private set; }
    
    private RA(){}

    private RA(string numero,
        string digitoVerificador,
        string uf)
    {
        Numero = numero;
        DigitoVerificador = digitoVerificador;
        UF = uf;
    }

    public static RA Create(
        string numero,
        string digitoVerificador,
        string uf)
    {
        ValidarNumero(numero);
        ValidarDigitoVerificador(digitoVerificador);
        ValidarUF(uf);

        var dvCalculado = CalcularDigitoVeriicador(numero);

        if (!string.Equals(dvCalculado, digitoVerificador, StringComparison.OrdinalIgnoreCase))
            throw new DomainException("O digito verificador do RA é inválido");
        
        return new RA(numero, digitoVerificador.ToUpperInvariant(), uf.ToUpperInvariant());
    }

    public string NumeroCompleto =>
        $"{Numero}-{DigitoVerificador}/{UF}";

    public static void ValidarNumero(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new DomainException("O número do RA é obrigatório.");

        if (!numero.All(char.IsDigit))
            throw new DomainException("O número do RA deve conter apenas números.");
    }

    public static void ValidarDigitoVerificador(string digitoVerificador)
    {
        if (string.IsNullOrWhiteSpace(digitoVerificador))
            throw new DomainException("O dígito verificador não pode ser nulo.");

        if (digitoVerificador.Length != 1)
            throw new DomainException("O digito verificador deve possuir um caractere.");

        if (!char.IsDigit(digitoVerificador[0]) && digitoVerificador.ToUpperInvariant() != "X")
            throw new DomainException("O digito verificador deve ser um número ou X.");
    }

    public static void ValidarUF(string uf)
    {
        if (string.IsNullOrWhiteSpace(uf))
            throw new DomainException("A UF do RA é obrigatória.");
        
        if(uf.Length != 2)
            throw new DomainException("A UF deve conter 2 letras.");
        
        if (!uf.All(char.IsLetter))
            throw new DomainException("A UF deve conter apenas letras.");
    }

    public static string CalcularDigitoVeriicador(string numero)
    {
        var numeros = numero.PadLeft(9 , '0')
            .TakeLast(9)
            .Select(c => c - '0')
            .ToArray();

        var soma = 0;
        
        for (int i = 0; i < 9; i++)
            soma += numeros[i] * (9 - i);
        
        var resto = soma  % 11;
        
        return resto == 10 ? "X"  : resto.ToString();
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Numero;
        yield return DigitoVerificador;
        yield return UF;
    }
}
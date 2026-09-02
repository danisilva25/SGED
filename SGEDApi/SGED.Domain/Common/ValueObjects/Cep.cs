namespace SGED.Domain.Common.ValueObjects;

public sealed class Cep : ValueObject
{
    public string Numero { get; private set; }

    private Cep(string numero)
    {
        Numero = numero;
    }

    public static Cep Criar(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException(
                "CEP não pode ser vazio.",
                nameof(numero));

        var cep = new string(
            numero.Where(char.IsDigit).ToArray());

        if (cep.Length != 8)
            throw new ArgumentException(
                "CEP deve possuir 8 dígitos.",
                nameof(numero));

        return new Cep(cep);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Numero;
    }
}
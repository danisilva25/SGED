namespace SGED.Domain.Common.ValueObjects;

public sealed class Phone : ValueObject
{
    public string Ddd { get; private set; }
    public string Numero { get; private set; }

    public bool EhCelular =>
        Numero.Length == 9;

    private Phone(string ddd, string numero)
    {
        Ddd = ddd;
        Numero = numero;
    }

    public static Phone Criar(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            throw new DomainException(
                "Telefone não pode ser vazio.");

        var numero = new string(
            telefone.Where(char.IsDigit).ToArray());

        if (numero.Length != 10 && numero.Length != 11)
            throw new DomainException(
                "Telefone deve possuir 10 ou 11 dígitos.");

        var ddd = numero[..2];
        var numeroLocal = numero[2..];

        ValidarDdd(ddd);

        if (numeroLocal.Length == 9)
            ValidarCelular(numeroLocal);
        else
            ValidarFixo(numeroLocal);

        return new Phone(ddd, numeroLocal);
    }

    private static void ValidarDdd(string ddd)
    {
        if (!int.TryParse(ddd, out var codigo))
            throw new DomainException(
                "DDD inválido.");

        if (codigo < 11 || codigo > 99)
            throw new DomainException(
                "DDD inválido.");
    }

    private static void ValidarCelular(string numero)
    {
        if (numero[0] != '9')
            throw new DomainException(
                "Telefone celular deve iniciar com o dígito 9.");
    }

    private static void ValidarFixo(string numero)
    {
        var primeiroDigito = numero[0];

        if (primeiroDigito < '2' || primeiroDigito > '5')
            throw new DomainException(
                "Telefone fixo deve iniciar com os dígitos de 2 a 5.");
    }

    public string NumeroCompleto =>
        $"{Ddd}{Numero}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Ddd;
        yield return Numero;
    }
}
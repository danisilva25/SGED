using System.Text.RegularExpressions;

namespace SGED.Domain.Common.ValueObjects;

public sealed class Email : ValueObject
{
    public string Endereco { get; private set; }

    private Email(string endereco)
    {
        Endereco = endereco;
    }

    public static Email Criar(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            throw new DomainException(
                "E-mail não pode ser vazio.");

        endereco = endereco.Trim();

        if (endereco.Length > 254)
            throw new DomainException(
                "E-mail não pode possuir mais de 254 caracteres.");

        if (!Regex.IsMatch(
                endereco,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.CultureInvariant))
        {
            throw new DomainException(
                "E-mail inválido.");
        }

        return new Email(endereco.ToLowerInvariant());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Endereco;
    }

    public override string ToString()
    {
        return Endereco;
    }
}
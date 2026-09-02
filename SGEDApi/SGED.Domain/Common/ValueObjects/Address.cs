namespace SGED.Domain.Common.ValueObjects;

public sealed class Address: ValueObject
{
    public string Street { get; }
    public string Number { get; }
    public string? Complement { get; }
    public string Neighborhood { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCodde { get; }

    private Address(
        string street,
        string number,
        string? complement,
        string neighborhood,
        string city,
        string state,
        string postalCodde)
    {
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        PostalCodde = postalCodde;
    }

    public static Address Criar(
        string street,
        string number,
        string? complement,
        string neighborhood,
        string city,
        string state,
        string postalCodde)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Logradouro obrigatório.");

        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Número obrigatório.");

        if (string.IsNullOrWhiteSpace(neighborhood))
            throw new ArgumentException("Bairro obrigatório.");

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Município obrigatório.");

        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("UF obrigatória.");

        if (string.IsNullOrWhiteSpace(postalCodde))
            throw new ArgumentException("CEP obrigatório.");

        return new Address(
            street.Trim(),
            number.Trim(),
            complement?.Trim(),
            neighborhood.Trim(),
            city.Trim(),
            state.Trim().ToUpperInvariant(),
            postalCodde.Trim());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return Number;
        yield return Complement ?? string.Empty;
        yield return Neighborhood;
        yield return City;
        yield return State;
        yield return PostalCodde;
    }
}
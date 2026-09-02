namespace SGED.Domain.Common.ValueObjects;

public sealed class ValidityPeriod : ValueObject
{
    public DateTime Inicio { get; }
    public DateTime? Fim { get; }

    private ValidityPeriod(DateTime inicio, DateTime? fim)
    {
        Inicio = inicio;
        Fim = fim;
    }

    public static ValidityPeriod CreateValidityPeriod(
        DateTime inicio,
        DateTime? fim = null)
    {
        if (fim.HasValue && fim.Value < inicio)
            throw new DomainException("A data final não pode ser anterior à data inicial");

        return new ValidityPeriod(inicio, fim);
    }

    public bool IsEffectiveDate(DateTime date)
    {
        if (date < Inicio)
            return false;

        if (Fim.HasValue && date > Fim.Value)
            return false;
        
        return true;
    }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Inicio;
        yield return Fim;
    }
}
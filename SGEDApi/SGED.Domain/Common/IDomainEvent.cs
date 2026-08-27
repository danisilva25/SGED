namespace SGED.Domain.Common;

public interface IDomainEvent
{
    Guid Id => Guid.NewGuid();
    DateTime OccurredOn => DateTime.UtcNow;
}
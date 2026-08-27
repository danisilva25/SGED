namespace SGED.Domain.Common;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent>  DomainEvents => _domainEvents;
    
    public int Version { get; protected set; }
    
    protected AggregateRoot(){}
    
    protected AggregateRoot(Guid id)
        :base(id) { }
    
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
    
    public void ClearDomainEvents() 
        => _domainEvents.Clear();
}
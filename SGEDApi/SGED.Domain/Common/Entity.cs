namespace SGED.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; protected set; }

    protected Entity(){}

    protected Entity(Guid id)
    {
        if(id == Guid.Empty)
            throw new DomainException("Id não pode ser vazio.");
        
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if(obj is not Entity other) 
            return false;

        if (ReferenceEquals(this, other))
            return true;
        
        if(GetType() != other.GetType()) 
            return false;
        
        return Id ==  other.Id;
    }
    
    public override int GetHashCode()
        => (GetType().ToString() + Id).GetHashCode();

    public static bool operator ==(Entity? left, Entity? right)
        => left is null && right is null || (left?.Equals(right) ??  false);
    
    public static bool operator != (Entity? left, Entity? right) 
        =>  !(left == right);
}
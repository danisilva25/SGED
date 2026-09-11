namespace SGED.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        if (id == Guid.Empty)
            throw new DomainException(
                "Id não pode ser vazio.");

        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is not Entity other)
            return false;

        if (GetType() != other.GetType())
            return false;

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            GetType(),
            Id);
    }

    public static bool operator ==(
        Entity? left,
        Entity? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(
        Entity? left,
        Entity? right)
    {
        return !(left == right);
    }
}
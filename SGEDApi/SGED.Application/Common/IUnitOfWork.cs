namespace SGED.Application.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync
        (CancellationToken cancellationToken);
}
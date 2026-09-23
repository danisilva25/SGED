using SGED.Domain.Curriculo.Bimestres;

namespace SGED.Application.Curriculo.Bimestres;

public interface IBimestreRepository
{
    Task AddAsync(Bimestre disciplina, CancellationToken cancellationToken); 
    Task<IReadOnlyList<Bimestre>> GetAllAsync(CancellationToken cancellationToken);
    Task<Bimestre?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
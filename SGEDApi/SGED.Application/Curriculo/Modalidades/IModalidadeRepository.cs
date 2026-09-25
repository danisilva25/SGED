using SGED.Domain.Curriculo.EtapasAnosEscolares;
using SGED.Domain.Curriculo.Modalidades;

namespace SGED.Application.Curriculo.Modalidades;

public interface IModalidadeRepository
{
    Task AddAsync(Modalidade modalidade, CancellationToken cancellationToken);
    Task<IReadOnlyList<Modalidade>> GetAllAsync(CancellationToken cancellationToken);
    Task<Modalidade?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
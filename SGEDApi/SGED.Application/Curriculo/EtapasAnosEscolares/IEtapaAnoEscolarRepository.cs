using SGED.Domain.Curriculo.EtapasAnosEscolares;

namespace SGED.Application.Curriculo.EtapasAnosEscolares;

public interface IEtapaAnoEscolarRepository
{
    Task AddAsync(EtapaAnoEscolar etapaAnoEscolar, CancellationToken cancellationToken);
    Task<IReadOnlyList<EtapaAnoEscolar>> GetAllAsync(CancellationToken cancellationToken);
    Task<EtapaAnoEscolar?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
using SGED.Domain.Common.Results;
using SGED.Domain.Curriculo.EtapasAnosEscolares;

namespace SGED.Application.Curriculo.EtapasAnosEscolares.ListarEtapasAnosEscolares;

public sealed class ListarEtapaAnoEscolarHandler(IEtapaAnoEscolarRepository repository)
{
    public async Task<Result<IReadOnlyList<EtapaAnoEscolar>>> Handle(CancellationToken cancellationToken)
    {
        var etapas = await repository.GetAllAsync(cancellationToken);
        
        return Result<IReadOnlyList<EtapaAnoEscolar>>.Success(etapas);
    }
}
using SGED.Domain.Common.Results;
using SGED.Domain.Curriculo.Modalidades;

namespace SGED.Application.Curriculo.Modalidades.ListarModalidades;

public sealed class ListarModalidadeHandler(IModalidadeRepository repository)
{
    public async Task<Result<IReadOnlyList<Modalidade>>> Handle(CancellationToken cancellationToken)
    {
        var modalidades = await repository.GetAllAsync(cancellationToken);
        
        return Result<IReadOnlyList<Modalidade>>.Success(modalidades);
    }
}
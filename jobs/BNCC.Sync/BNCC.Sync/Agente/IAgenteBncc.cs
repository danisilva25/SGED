using Infrastructure.Json.Models;

namespace BNCC.Sync.Agente;

public interface IAgenteBncc
{
    Task<Root> ExtrairAsync(
        string conteudoFonte,
        CancellationToken cancellationToken = default);
}
namespace BNCC.Sync.Agente;

public sealed class AgenteBnccComFallback(
    IAgenteBncc ollama,
    IAgenteBncc gemini) : IAgenteBncc
{
    public async Task<Infrastructure.Json.Models.Root> ExtrairAsync(
        string conteudoFonte,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Console.WriteLine("Iniciando extração com Ollama...");

            var resultado = await ollama.ExtrairAsync(
                conteudoFonte,
                cancellationToken);

            Console.WriteLine("Extração concluída com Ollama.");

            return resultado;
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception erroOllama)
        {
            Console.WriteLine(
                $"Ollama falhou: {erroOllama.Message}");

            Console.WriteLine(
                "Acionando Gemini como alternativa...");

            try
            {
                var resultado = await gemini.ExtrairAsync(
                    conteudoFonte,
                    cancellationToken);

                Console.WriteLine(
                    "Extração concluída com Gemini.");

                return resultado;
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception erroGemini)
            {
                throw new AggregateException(
                    "A extração falhou nos dois provedores. " +
                    "Ollama e Gemini não conseguiram processar o lote.",
                    erroOllama,
                    erroGemini);
            }
        }
    }
}

using System.Text;
using System.Text.Json;
using Domain.Mensagens;
using Infrastructure.Persistence;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.RabbitMq;

public sealed class RabbitMqConsumer(
    IChannel channel,
    string queue,
    ControleProcessamentoBncc controle,
    Func<int, int, CancellationToken, Task> processarLote,
    RabbitMqPublisher publicador,
    int paginasPorLote,
    Func<CancellationToken, Task> finalizarAsync)
{
    public async Task ConsumirAsync(
        CancellationToken cancellationToken = default)
    {
        await channel.BasicQosAsync(
            prefetchSize: 0,
            prefetchCount: 1,
            global: false,
            cancellationToken: cancellationToken);

        var falha = new TaskCompletionSource<Exception>(
            TaskCreationOptions.RunContinuationsAsynchronously);

        var consumidor = new AsyncEventingBasicConsumer(channel);

        consumidor.ReceivedAsync += async (_, evento) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(evento.Body.Span);

                var mensagem = JsonSerializer.Deserialize<ExtrairLoteBncc>(json)
                    ?? throw new InvalidOperationException(
                        "Mensagem RabbitMQ inválida.");

                var estado = await controle.ObterAsync(cancellationToken)
                    ?? throw new InvalidOperationException(
                        "Controle de processamento não encontrado.");
                
                if (mensagem.PaginaInicial < estado.PaginaInicial)
                {
                    Console.WriteLine(
                        $"Lote {mensagem.PaginaInicial}-{mensagem.PaginaFinal} " +
                        "já concluído. " +
                        "Ignorando reentrega.");

                    await channel.BasicAckAsync(
                        evento.DeliveryTag,
                        multiple: false,
                        cancellationToken);

                    return;
                }

                if (estado.Status == "Concluido")
                {
                    await finalizarAsync(cancellationToken);

                    await channel.BasicAckAsync(
                        evento.DeliveryTag,
                        multiple: false,
                        cancellationToken);

                    return;
                }

                if (mensagem.PaginaInicial != estado.PaginaInicial ||
                    mensagem.PaginaFinal != estado.PaginaFinal)
                    throw new InvalidOperationException(
                        $"Lote recebido {mensagem.PaginaInicial}-" +
                        $"{mensagem.PaginaFinal} não corresponde ao intervalo " +
                        $"pendente {estado.PaginaInicial}-{estado.PaginaFinal}.");

                await processarLote(
                    mensagem.PaginaInicial,
                    mensagem.PaginaFinal,
                    cancellationToken);
                
                var estadoAtualizado =
                    await controle.ObterAsync(cancellationToken)
                    ?? throw new InvalidOperationException(
                        "Controle desapareceu após processar o lote.");

                if (estadoAtualizado.Status == "Concluido")
                    await finalizarAsync(cancellationToken);
                else
                    await publicador.PublicarAsync(
                        new ExtrairLoteBncc(
                            estadoAtualizado.PaginaInicial,
                            estadoAtualizado.PaginaFinal),
                        cancellationToken);

                await channel.BasicAckAsync(
                    evento.DeliveryTag,
                    multiple: false,
                    cancellationToken);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                await channel.BasicNackAsync(
                    evento.DeliveryTag,
                    multiple: false,
                    requeue: true,
                    CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    $"Falha no processamento BNCC: {ex.Message}");

                try
                {
                    var status = PareceCotaGemini(ex)
                        ? "AguardandoCota"
                        : "Erro";

                    await controle.RegistrarErroAsync(
                        status,
                        ex.ToString(),
                        CancellationToken.None);
                }
                catch (Exception erroBanco)
                {
                    Console.Error.WriteLine(
                        "Não foi possível registrar o erro no PostgreSQL: " +
                        erroBanco.Message);
                }

                try
                {
                    await channel.BasicNackAsync(
                        evento.DeliveryTag,
                        multiple: false,
                        requeue: false,
                        CancellationToken.None);
                }
                finally
                {
                    falha.TrySetResult(ex);
                }
            }
        };

        await channel.BasicConsumeAsync(
            queue: queue,
            autoAck: false,
            consumer: consumidor,
            cancellationToken: cancellationToken);

        var esperaCancelamento = Task.Delay(
            Timeout.Infinite,
            cancellationToken);

        var concluido = await Task.WhenAny(
            esperaCancelamento,
            falha.Task);

        if (concluido == falha.Task)
            throw new InvalidOperationException(
                "O consumo foi interrompido após uma falha. " +
                "O lote pendente foi preservado no controle de processamento.",
                await falha.Task);

        await esperaCancelamento;
    }

    private static bool PareceCotaGemini(Exception ex)
    {
        for (Exception? atual = ex; atual is not null; atual = atual.InnerException)
        {
            var mensagem = atual.Message;

            if (mensagem.Contains("RESOURCE_EXHAUSTED",
                    StringComparison.OrdinalIgnoreCase) ||
                mensagem.Contains("quota",
                    StringComparison.OrdinalIgnoreCase) ||
                mensagem.Contains("rate limit",
                    StringComparison.OrdinalIgnoreCase) ||
                mensagem.Contains("429",
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}

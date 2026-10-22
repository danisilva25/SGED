using System.Text;
using System.Text.Json;
using Domain.Mensagens;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.RabbitMq;

public sealed class RabbitMqConsumer(
    IChannel channel,
    string queue)
{
    public async Task ConsumirAsync(
        Func<ExtrairLoteBncc, CancellationToken, Task> processar,
        CancellationToken cancellationToken = default)
    {
        await channel.BasicQosAsync(
            prefetchSize: 0,
            prefetchCount: 1,
            global: false,
            cancellationToken: cancellationToken);

        var consumidor = new AsyncEventingBasicConsumer(channel);

        consumidor.ReceivedAsync += async (_, evento) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(evento.Body.Span);

                var mensagem =
                    JsonSerializer.Deserialize<ExtrairLoteBncc>(json)
                    ?? throw new InvalidOperationException(
                        "Mensagem RabbitMQ inválida.");

                await processar(mensagem, cancellationToken);

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
                    $"Falha ao processar mensagem: {ex.Message}");

                await channel.BasicNackAsync(
                    evento.DeliveryTag,
                    multiple: false,
                    requeue: true,
                    CancellationToken.None);
            }
        };

        await channel.BasicConsumeAsync(
            queue: queue,
            autoAck: false,
            consumer: consumidor,
            cancellationToken: cancellationToken);

        await Task.Delay(
            Timeout.Infinite,
            cancellationToken);
    }
}
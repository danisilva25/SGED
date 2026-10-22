using System.Text;
using System.Text.Json;
using Domain.Mensagens;
using RabbitMQ.Client;

namespace Infrastructure.RabbitMq;

public sealed class RabbitMqPublisher(
    IChannel channel,
    string queue)
{
    public async Task PublicarAsync(
        ExtrairLoteBncc mensagem,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(mensagem);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json"
        };

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queue,
            mandatory: true,
            basicProperties: properties,
            body: body,
            cancellationToken: cancellationToken);
    }
}
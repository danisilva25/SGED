using RabbitMQ.Client;

namespace Infrastructure.RabbitMq;

public sealed class RabbitMqConnection : IAsyncDisposable
{
    private readonly IConnection _connection;

    private RabbitMqConnection(IConnection connection)
    {
        _connection = connection;
    }

    public static async Task<RabbitMqConnection> CreateAsync(
        RabbitMqOptions options,
        CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory
        {
            HostName = options.Host,
            Port = options.Port,
            UserName = options.Username,
            Password = options.Password,
            VirtualHost = options.VirtualHost
        };

        var connection = await factory.CreateConnectionAsync(
            cancellationToken);

        return new RabbitMqConnection(connection);
    }

    public async Task<IChannel> CreateChannelAsync(
        string queue,
        CancellationToken cancellationToken = default)
    {
        var channel = await _connection.CreateChannelAsync(
            cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        return channel;
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
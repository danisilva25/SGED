namespace Infrastructure.RabbitMq;


public sealed class RabbitMqOptions
{
    public string Host { get; init; } = Environment.GetEnvironmentVariable("RABBITMQ_HOST")!;
    public int Port { get; init; } = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT")!);
    public string Username { get; init; } = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME")!;
    public string Password { get; init; } = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")!;
    public string VirtualHost { get; init; } = Environment.GetEnvironmentVariable("RABBITMQ_VHOST")!;
    public string Queue { get; init; } = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE")!;
}
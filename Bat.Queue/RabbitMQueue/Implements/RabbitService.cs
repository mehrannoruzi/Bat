namespace Bat.Queue;

public class RabbitService : IRabbitService
{
    private readonly RabbitConfiguration _rabbitConfiguration;

    public RabbitService(IOptions<RabbitConfiguration> rabbitConfiguration)
    {
        _rabbitConfiguration = rabbitConfiguration.Value;
    }

    public IConnection CreateConnection()
    {
        var connection = new ConnectionFactory
        {
            UserName = _rabbitConfiguration.Username,
            Password = _rabbitConfiguration.Password,
            HostName = _rabbitConfiguration.HostName,
            DispatchConsumersAsync = true
        };
        connection.Port = _rabbitConfiguration.Port > 0 ? _rabbitConfiguration.Port : connection.Port;

        return connection.CreateConnection();
    }

    public IConnection CreateConnection(ConnectionFactory connectionFactory)
        => connectionFactory.CreateConnection();
}
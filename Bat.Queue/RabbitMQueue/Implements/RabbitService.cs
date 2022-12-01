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

        return connection.CreateConnection();
    }
}
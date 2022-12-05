namespace Bat.Queue;

public interface IRabbitService
{
    IConnection CreateConnection();
    IConnection CreateConnection(ConnectionFactory connectionFactory);
}
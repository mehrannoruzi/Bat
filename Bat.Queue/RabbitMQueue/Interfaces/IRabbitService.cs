namespace Bat.Queue;

public interface IRabbitService
{
    IConnection CreateConnection();
}
using RabbitMQ.Client;

namespace Bat.Queue.RabbitMQueue.Implements;

public class Producer
{
    private static IModel _channel;
    private static readonly object _lock = new();
    private volatile static Producer _producerInstance;
    private static ConnectionFactory _connectionFactory;

    private Producer() { }

    public static Producer GetInstance(ConnectionFactory connectionFactory)
    {
        lock (_lock)
        {
            if (_producerInstance != null && _channel != null) return _producerInstance;

            _connectionFactory = connectionFactory;
            _producerInstance = new Producer();
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: connectionFactory.UserName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel = channel;
            return _producerInstance;
        }
    }

    public static void PublishMessage(string message)
    {
        _channel.BasicPublish(exchange: "",
                             routingKey: _connectionFactory.UserName,
                             basicProperties: null,
                             body: Encoding.UTF8.GetBytes(message));
    }

}
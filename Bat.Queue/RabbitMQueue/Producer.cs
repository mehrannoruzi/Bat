using global::RabbitMQ.Client;

namespace Bat.Queue;

public class Producer
{
    private static IModel channel;
    private static ConnectionFactory factory;
    private volatile static Producer instance;
    private static readonly object padlock = new object();

    private Producer() { }

    public static Producer GetInstance(ConnectionFactory connectionFactory)
    {
        lock (padlock)
        {
            if (instance != null && channel != null) return instance;

            factory = connectionFactory;
            instance = new Producer();
            var connection = factory.CreateConnection();
            var ch = connection.CreateModel();

            ch.QueueDeclare(queue: connectionFactory.UserName,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
            channel = ch;
            return instance;
        }
    }

    public static void PublishMessage(string message)
    {
        channel.BasicPublish(exchange: "",
                             routingKey: factory.UserName,
                             basicProperties: null,
                             body: Encoding.UTF8.GetBytes(message));
    }

}
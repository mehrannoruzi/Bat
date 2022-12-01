namespace Bat.Queue;

public class Consumer
{
    public static void Initilize(ConnectionFactory connectionFactory, Action<string> action)
    {
        var connection = connectionFactory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: connectionFactory.UserName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            action.Invoke(Encoding.UTF8.GetString(ea.Body.ToArray()));
        };

        channel.BasicConsume(queue: connectionFactory.UserName,
                             autoAck: false,
                             consumer: consumer);
    }
}
namespace Bat.Queue;

public interface IRabbitConsumer : IDisposable
{
    Task Subscribe(Action<string, object> receiveMessageAction, string queueName = null, string exchangeName = null,
        string routingKey = "", RabbitExchangeType exchangeType = RabbitExchangeType.Direct, bool durable = true,
        bool autoDelete = false, string consumerTag = "", IDictionary<string, object> arguments = null);
}
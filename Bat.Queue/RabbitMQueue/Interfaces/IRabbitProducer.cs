namespace Bat.Queue;

public interface IRabbitProducer : IDisposable
{
    bool Publish<T>(T message, string exchangeName = null, string routingKey = "", bool mandatory = false, IBasicProperties basicProperties = null);
}
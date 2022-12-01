namespace Bat.Queue;

public interface IRabbitProducer : IDisposable
{
    bool Publish<T>(T message, string queueName = null);
}
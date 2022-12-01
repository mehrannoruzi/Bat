namespace Bat.Queue;

public interface IRabbitConsumer : IDisposable
{
    void Subscribe(Action<string> receiveMessageAction, string queueName = null);
}
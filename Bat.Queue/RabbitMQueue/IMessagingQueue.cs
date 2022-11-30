namespace Bat.Queue;

public interface IMessagingQueue
{
    bool Send<T>(string queueName, T message);

    void Receive<T>(string queueName, EventHandler callbackMethod);
}
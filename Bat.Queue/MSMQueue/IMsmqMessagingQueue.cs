namespace Bat.Queue;

using Experimental.System.Messaging;

public interface IMsmQueue : IMessagingQueue
{
    new bool Send<T>(string queueName, T message);

    void Receive<T>(string queueName, ReceiveCompletedEventHandler callbackMethod);

}
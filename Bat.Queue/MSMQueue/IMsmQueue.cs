using Experimental.System.Messaging;

namespace Bat.Queue;

public interface IMsmQueue
{
    bool Send<T>(string queueName, T message);

    void Receive<T>(string queueName, ReceiveCompletedEventHandler callbackMethod);

}
namespace Bat.MessagingQueue
{
    using Experimental.System.Messaging;

    public interface IMsmQueue : IMessagingQueue
    {
        /// <summary>
        /// Add Message 
        /// </summary>
        /// <param name="model">send Message model</param>
        /// <returns>Insert Message status</returns>
        new bool Send<T>(string queueName, T message);

        /// <summary>
        /// Get Top 'take' UnProccessed Message
        /// </summary>
        /// <param name="take">count of UnProccessed message in queue</param>
        /// <returns>list of UnProccessed message</returns>
        void Receive<T>(string queueName, ReceiveCompletedEventHandler callbackMethod);
    }
}

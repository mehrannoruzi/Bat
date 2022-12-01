namespace Bat.Queue;

public class RabbitConsumer : IRabbitConsumer
{
    private IModel _model;
    private IConnection _connection;
    private string _queueName = "Rabbit";
    private readonly IRabbitService _rabbitService;

    public RabbitConsumer(IRabbitService rabbitService)
    {
        _rabbitService = rabbitService;
    }

    public void Subscribe(Action<string> receiveMessageAction, string queueName = null)
    {
        var connection = _rabbitService.CreateConnection();
        _model = connection.CreateModel();

        if (!string.IsNullOrWhiteSpace(queueName)) _queueName = queueName;
        _model.QueueDeclare($"{_queueName}_Queue", durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare($"{_queueName}_Exchange", ExchangeType.Fanout, durable: true, autoDelete: false);
        _model.QueueBind($"{_queueName}_Queue", $"{_queueName}_Exchange", string.Empty);

        var consumer = new EventingBasicConsumer(_model);
        consumer.Received += (model, eventArgs) =>
        {
            receiveMessageAction.Invoke(Encoding.UTF8.GetString(eventArgs.Body.ToArray()));
        };

        _model.BasicConsume(
            queue: $"{_queueName}_Queue",
            autoAck: true,
            consumer: consumer);
    }

    public void Dispose()
    {
        if (_model.IsOpen) _model.Close();
        _model.Dispose();

        if (_connection.IsOpen) _connection.Close();
        _connection.Dispose();

        GC.SuppressFinalize(this);
    }
}
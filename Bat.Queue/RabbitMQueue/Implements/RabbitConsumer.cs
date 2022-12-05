namespace Bat.Queue;

public class RabbitConsumer : IRabbitConsumer
{
    private IModel _model;
    private IConnection _connection;
    private string _queueName = "Bat_Queue";
    private string _exchangeName = "Bat_Exchange";
    private readonly IRabbitService _rabbitService;

    public RabbitConsumer(IRabbitService rabbitService)
    {
        _rabbitService = rabbitService;
    }


    public async Task Subscribe(Action<string, object> receiveEventAction, string queueName = null, string exchangeName = null,
        string routingKey = "", RabbitExchangeType exchangeType = RabbitExchangeType.Direct, bool durable = true,
        bool autoDelete = false, string consumerTag = "", IDictionary<string, object> arguments = null)
    {
        _connection = _rabbitService.CreateConnection();
        _model = _connection.CreateModel();

        if (!string.IsNullOrWhiteSpace(queueName)) _queueName = $"{queueName}_Queue";
        if (!string.IsNullOrWhiteSpace(exchangeName)) _exchangeName = $"{exchangeName}_Exchange";

        _model.QueueDeclare(_queueName, durable: durable, exclusive: false, autoDelete: autoDelete);

        if (exchangeType == RabbitExchangeType.Direct)
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: durable, autoDelete: autoDelete);
        else if (exchangeType == RabbitExchangeType.Fanout)
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Fanout, durable: durable, autoDelete: autoDelete);
        else if (exchangeType == RabbitExchangeType.Headers)
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Headers, durable: durable, autoDelete: autoDelete);
        else if (exchangeType == RabbitExchangeType.Topic)
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Topic, durable: durable, autoDelete: autoDelete);

        _model.QueueBind(_queueName, _exchangeName, routingKey);

        var consumer = new AsyncEventingBasicConsumer(_model);
        consumer.Received += async (model, eventArgs) =>
        {
            await Task.Run(() => receiveEventAction.Invoke(Encoding.UTF8.GetString(eventArgs.Body.ToArray()), eventArgs));
        };

        _model.BasicConsume(
            consumer: consumer,
            queue: _queueName,
            autoAck: true,
            consumerTag: consumerTag,
            arguments: arguments);
        await Task.CompletedTask;
    }

    public async Task Subscribe(IConnection connection, Action<string, object> receiveEventAction, string queueName = null, string exchangeName = null,
        string routingKey = "", RabbitExchangeType exchangeType = RabbitExchangeType.Direct, bool durable = true,
        bool autoDelete = false, string consumerTag = "", IDictionary<string, object> arguments = null)
    {
        _connection = connection;
        _model = connection.CreateModel();

        if (!string.IsNullOrWhiteSpace(queueName)) _queueName = $"{queueName}_Queue";
        if (!string.IsNullOrWhiteSpace(exchangeName)) _exchangeName = $"{exchangeName}_Exchange";

        _model.QueueDeclare(_queueName, durable: durable, exclusive: false, autoDelete: autoDelete);

        if (exchangeType == RabbitExchangeType.Direct)
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: durable, autoDelete: autoDelete);
        else if (exchangeType == RabbitExchangeType.Fanout)
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Fanout, durable: durable, autoDelete: autoDelete);
        else if (exchangeType == RabbitExchangeType.Headers)
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Headers, durable: durable, autoDelete: autoDelete);
        else if (exchangeType == RabbitExchangeType.Topic)
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Topic, durable: durable, autoDelete: autoDelete);

        _model.QueueBind(_queueName, _exchangeName, routingKey);

        var consumer = new AsyncEventingBasicConsumer(_model);
        consumer.Received += async (model, eventArgs) =>
        {
            await Task.Run(() => receiveEventAction.Invoke(Encoding.UTF8.GetString(eventArgs.Body.ToArray()), eventArgs));
        };

        _model.BasicConsume(
            consumer: consumer,
            queue: _queueName,
            autoAck: true,
            consumerTag: consumerTag,
            arguments: arguments);
        await Task.CompletedTask;
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
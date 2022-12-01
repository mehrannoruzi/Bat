namespace Bat.Queue;

public class RabbitProducer : IRabbitProducer
{
    private IModel _model;
    private IConnection _connection;
    private string _queueName = "Rabbit";
    private readonly IRabbitService _rabbitService;

    public RabbitProducer(IRabbitService rabbitService)
    {
        _rabbitService = rabbitService;
    }

    public bool Publish<T>(T message, string queueName = null)
    {
        try
        {
            _connection = _rabbitService.CreateConnection();
            _model = _connection.CreateModel();

            if (!string.IsNullOrWhiteSpace(queueName)) _queueName = queueName;
            _model.QueueDeclare($"{_queueName}_Queue");
            _model.BasicPublish(
                exchange: $"{_queueName}_Exchange",
                routingKey: "",
                body: Encoding.UTF8.GetBytes(message.SerializeToJson()));

            return true;
        }
        catch
        {
            return false;
        }
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
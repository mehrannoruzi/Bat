namespace Bat.Queue;

public class RabbitProducer(IRabbitService rabbitService) : IRabbitProducer, IDisposable
{
	private IModel _model;
	private IConnection _connection;
	private string _exchangeName = "Bat_Exchange";
	private readonly IRabbitService _rabbitService = rabbitService;

	public bool Publish<T>(T message, string exchangeName = null, string routingKey = "",
		bool mandatory = false, IBasicProperties basicProperties = null)
	{
		_connection = _rabbitService.CreateConnection();
		_model = _connection.CreateModel();

		if (!string.IsNullOrWhiteSpace(exchangeName)) _exchangeName = $"{exchangeName}_Exchange";

		_model.BasicPublish(
			exchange: _exchangeName,
			routingKey: routingKey,
			mandatory: mandatory,
			basicProperties: basicProperties,
			body: Encoding.UTF8.GetBytes(message.SerializeToJson()));

		return true;
	}

	public bool Publish<T>(IConnection connection, T message, string exchangeName = null, string routingKey = "",
		bool mandatory = false, IBasicProperties basicProperties = null)
	{
		_connection = connection;
		_model = _connection.CreateModel();

		if (!string.IsNullOrWhiteSpace(exchangeName)) _exchangeName = $"{exchangeName}_Exchange";

		_model.BasicPublish(
			exchange: _exchangeName,
			routingKey: routingKey,
			mandatory: mandatory,
			basicProperties: basicProperties,
			body: Encoding.UTF8.GetBytes(message.SerializeToJson()));

		return true;
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
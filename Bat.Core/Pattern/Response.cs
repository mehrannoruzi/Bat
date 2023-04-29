namespace Bat.Core;

public class Response : IResponse
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
	public int ResultCode { get; set; } = 200;
	public DateTime ExecutionTime => DateTime.Now;

	public Response()
	{ }

	public Response(string errorMessage)
	{
		IsSuccess = false;
		Message = errorMessage;
	}

	public Response(string errorMessage, int resultCode)
	{
		IsSuccess = false;
		Message = errorMessage;
		ResultCode = resultCode;
	}


	public static Response Error(string errorMessage)
		=> new(errorMessage);

	public static Response Error(string errorMessage, int resultCode)
		=> new(errorMessage, resultCode);

	public static Response Success(string message)
		=> new() { IsSuccess = true, Message = message };

	public static Response Success(string message, int resultCode)
		=> new() { IsSuccess = true, Message = message, ResultCode = resultCode };

}
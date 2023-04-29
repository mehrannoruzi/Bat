namespace Bat.Core;

public class Response<T> : IResponse<T>
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
	public T Result { get; set; }
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

	public Response(T result, string message = null)
	{
		IsSuccess = true;
		Result = result;
		Message = message;
	}


	public static Response<T> Error(string errorMessage)
		=> new(errorMessage);

	public static Response<T> Error(string errorMessage, int resultCode)
		=> new(errorMessage, resultCode);

	public static Response<T> Success(T result, string message = null)
		=> new() { IsSuccess = true, Result = result, Message = message };

	public static Response<T> Success(T result, string message, int resultCode)
		=> new() { IsSuccess = true, Result = result, Message = message, ResultCode = resultCode };

}
namespace Bat.Core;

public class Response<T> : IResponse<T>
{
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
        Message = message;
        Result = result;
    }


    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Result { get; set; }
    public int ResultCode { get; set; } = 200;
    public DateTime ExecutionTime => DateTime.Now;
}
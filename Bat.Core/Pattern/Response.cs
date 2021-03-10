namespace Bat.Core
{
    public class Response<T> : IResponse<T>
    {
        public Response()
        { }

        public Response(string errorMessage)
        {
            IsSuccessful = false;
            Message = errorMessage;
        }

        public Response(T result, string message = null)
        {
            IsSuccessful = true;
            Message = message;
            Result = result;
        }

        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public int ResultCode { get; set; } = 200;
    }
}
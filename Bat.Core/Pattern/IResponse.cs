namespace Bat.Core;

public interface IResponse<TResult>
{
    bool IsSuccessful { get; set; }
    string Message { get; set; }
    TResult Result { get; set; }
    int ResultCode { get; set; }
}
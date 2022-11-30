namespace Bat.Core;

public interface IResponse<TResult>
{
    bool IsSuccess { get; set; }
    string Message { get; set; }
    TResult Result { get; set; }
    int ResultCode { get; set; }
    DateTime ExecutionTime { get; }
}
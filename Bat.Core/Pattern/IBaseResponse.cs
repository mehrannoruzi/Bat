namespace Bat.Core;

public interface IBaseResponse
{
    bool IsSuccess { get; set; }
    string Message { get; set; }
    int ResultCode { get; set; }
    DateTime ExecutionTime { get; }
}
namespace Bat.Core;

public interface IBaseResponse
{
	bool IsSuccessful { get; set; }
	string Message { get; set; }
	int ResultCode { get; set; }
	DateTime ExecutionTime { get; }
}
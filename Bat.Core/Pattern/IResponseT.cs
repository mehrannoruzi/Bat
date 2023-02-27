namespace Bat.Core;

public interface IResponse<TResult> : IBaseResponse
{
    TResult Result { get; set; }
}
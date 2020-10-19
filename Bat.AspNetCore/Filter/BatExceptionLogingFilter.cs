using Bat.Core;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bat.AspNetCore
{
    public class BatExceptionLogingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            FileLoger.Error(context.Exception);
        }
    }
}
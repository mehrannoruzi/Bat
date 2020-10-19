using Bat.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Bat.AspNetCore
{
    public class BatExceptionFilter : IExceptionFilter
    {
        private IConfiguration _configuration { get; }

        public BatExceptionFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnException(ExceptionContext context)
        {
            FileLoger.Error(context.Exception);

            var errorRedirectUrl = _configuration.GetSection("CustomSettings")?["ErrorUrl"];
            if (string.IsNullOrWhiteSpace(errorRedirectUrl))
                context.HttpContext.Response.Redirect($"/Error/Details?code={context.HttpContext.Response.StatusCode}");
            else
                context.HttpContext.Response.Redirect($"{errorRedirectUrl}?code={context.HttpContext.Response.StatusCode}");
        }
    }
}
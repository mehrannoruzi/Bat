using System;
using Bat.Core;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Bat.AspNetCore
{
    public class BatExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BatExceptionHandlingMiddleware> _logger;

        public BatExceptionHandlingMiddleware(RequestDelegate next, ILogger<BatExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var requestBody = await context.Request.ReadRequestBody();
            _logger.LogError(ex, $"Url: {context.Request.Path}, QueryString: {context.Request.QueryString.Value}, RequestBody: {requestBody}");

            byte[] response;
            if (ex is DomainException)
            {
                response = Encoding.UTF8.GetBytes(new
                {
                    resultCode = (int)HttpStatusCode.BadRequest,
                    isSuccessful = false,
                    message = "اطلاعات وارد شده صحیح نمی باشد، لطفا مجددا تلاش نمایید."
                }.SerializeToJson());
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (ex is ServiceException)
            {
                response = Encoding.UTF8.GetBytes(new
                {
                    resultCode = (int)HttpStatusCode.BadRequest,
                    isSuccessful = false,
                    message = "عملیات مورد نظر با خطا رو به رو شده است، لطفا مجددا تلاش نمایید."
                }.SerializeToJson());
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                response = Encoding.UTF8.GetBytes(new
                {
                    resultCode = (int)HttpStatusCode.InternalServerError,
                    isSuccessful = false,
                    message = "عملیات مورد نظر با خطا رو به رو شده است، لطفا مجددا تلاش نمایید."
                }.SerializeToJson());
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.Response.ContentType = "application/Json";
            await context.Response.Body.WriteAsync(response);
        }
    }
}
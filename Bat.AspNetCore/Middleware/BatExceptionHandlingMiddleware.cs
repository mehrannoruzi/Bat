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
            _logger.LogError(ex, $"Url: {context.Request.Path}, QueryString: {context.Request.QueryString.Value}");

            byte[] response;
            if (ex is DomainException)
            {
                response = Encoding.UTF8.GetBytes(new Response<object>
                {
                    ResultCode = (int)HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    Message = "اطلاعات وارد شده صحیح نمی باشد، لطفا مجددا تلاش نمایید."
                }.SerializeToJson());
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (ex is ServiceException)
            {
                response = Encoding.UTF8.GetBytes(new Response<object>
                {
                    ResultCode = (int)HttpStatusCode.BadRequest,
                    IsSuccessful = false,
                    Message = "عملیات مورد نظر با خطا رو به رو شده است، لطفا مجددا تلاش نمایید."
                }.SerializeToJson());
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                response = Encoding.UTF8.GetBytes(new Response<object>
                {
                    ResultCode = (int)HttpStatusCode.InternalServerError,
                    IsSuccessful = false,
                    Message = "عملیات مورد نظر با خطا رو به رو شده است، لطفا مجددا تلاش نمایید."
                }.SerializeToJson());
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.Response.ContentType = "application/Json";
            await context.Response.Body.WriteAsync(response, 0, response.Length);
        }
    }
}
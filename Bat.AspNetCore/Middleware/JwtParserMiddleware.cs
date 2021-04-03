using System;
using Bat.Core;
using System.Net;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Bat.AspNetCore
{
    public class JwtParserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;

        public JwtParserMiddleware(RequestDelegate next, IJwtService jwtService,
            IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtService = jwtService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            try
            {
                if (token != null)
                {
                    if (_jwtService.IsNull())
                    {
                        var response = Encoding.UTF8.GetBytes(new Response<object>
                        {
                            ResultCode = 1001,
                            IsSuccessful = false,
                            Message = "Jwt Service Not Configure !"
                        }.SerializeToJson());

                        context.Response.ContentType = "application/Json";
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.Body.WriteAsync(response, 0, response.Length);
                    }

                    var userClaims = _jwtService.GetClaimsPrincipal(token, _jwtSettings);
                    if (userClaims != null) context.User = userClaims;
                }
                else
                {
                    if (context.User.Claims.Any())
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/Json";
                        var bytes = Encoding.UTF8.GetBytes(new Response<object> { IsSuccessful = false, Message = "UnAuthorized Access To Api !. Token Not Sent.", ResultCode = 401 }.SerializeToJson());
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }

                await _next(context);
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                byte[] response;
                if (e.Message.Contains("Lifetime validation failed"))
                {
                    #region Expired Token
                    //var validationTime = _jwtService.GetTokenExpireTime(token, _jwtSettings);
                    response = Encoding.UTF8.GetBytes(new Response<object>
                    {
                        ResultCode = 1001,
                        IsSuccessful = false,
                        Message = "کاربر گرامی، توکن اعتبارسنجی شما منقضی شده است. لطفا مجدد وارد سامانه شوید."
                    }.SerializeToJson());
                    #endregion
                }
                else
                {
                    #region Another Exception
                    response = Encoding.UTF8.GetBytes(new Response<object>
                    {
                        ResultCode = 1002,
                        IsSuccessful = false,
                        Message = "کاربر گرامی، توکن اعتبارسنجی شما منقضی شده است. لطفا مجدد وارد سامانه شوید." +
                        Environment.NewLine + e.Message
                    }.SerializeToJson());
                    #endregion
                }

                context.Response.ContentType = "application/Json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.Body.WriteAsync(response, 0, response.Length);
            }
        }
    }
}
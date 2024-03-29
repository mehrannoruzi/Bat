﻿using System.Net;
using Microsoft.Extensions.Options;
using System;

namespace Bat.AspNetCore;

public class BatJwtParserMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;

    public BatJwtParserMiddleware(RequestDelegate next, IJwtService jwtService,
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
                    var response = Encoding.UTF8.GetBytes(new
                    {
                        resultCode = 1001,
                        isSuccessful = false,
                        message = "Jwt Service Not Configure !"
                    }.SerializeToJson());

                    context.Response.ContentType = "application/Json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.Body.WriteAsync(response);
                }

                var userClaims = _jwtService.GetClaimsPrincipal(token, _jwtSettings);
                if (userClaims != null) context.User = userClaims;
            }
            else
            {
                if (context.User.Claims.Any())
                {
                    context.Response.ContentType = "application/Json";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    var bytes = Encoding.UTF8.GetBytes(new
                    {
                        resultCode = 401,
                        isSuccessful = false,
                        message = "UnAuthorized Access To Api !. Token Not Sent.",
                    }.SerializeToJson());
                    await context.Response.Body.WriteAsync(bytes);
                }
            }

            await _next(context);
        }
        catch (Exception e)
        {
            byte[] response;
            if (e.Message.Contains("Lifetime validation failed"))
            {
                #region Expired Token
                //var validationTime = _jwtService.GetTokenExpireTime(token, _jwtSettings);
                response = Encoding.UTF8.GetBytes(new
                {
                    resultCode = 1001,
                    isSuccessful = false,
                    message = "کاربر گرامی، مدت زمان زیادی است که از سامانه استفاده نکرده اید. لطفا مجدد وارد سامانه شوید."
                }.SerializeToJson());
                #endregion
            }
            else
            {
                FileLoger.Error(e);

                #region Another Exception
                response = Encoding.UTF8.GetBytes(new
                {
                    resultCode = 1002,
                    isSuccessful = false,
                    message = "کاربر گرامی، عملیات موردنظر با خطا رو به رو شده است. لطفا مجدد تلاش کنید."
                }.SerializeToJson());
                #endregion
            }

            context.Response.ContentType = "application/Json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.Body.WriteAsync(response);
        }
    }
}
For use Bat.AspNetCore just do it :

1- Install Bat.AspNetCore on your project

2- Use it in your project
for example :


    // Configure the Services.
    builder.Services.AddBatAuthentication();

    builder.Services.AddBatJwtConfiguration(_jwtSettings);

    builder.Services.AddMemoryCache();

    builder.Services.Configure<JwtSettings>(_configuration.GetSection("JwtSetting"));
    builder.Services.AddTransient<IJwtService, JwtService>();

    builder.Services.AddBatSwagger(_swaggerSetting);


    // Configure the HTTP request pipeline.
    app.UseMiddleware<BatJwtParserMiddleware>();

    app.UseMiddleware<BatEnableRequestBufferingMiddleware>();

    app.UseMiddleware<BatExceptionHandlingMiddleware>();

    app.UseBatJwtConfiguration();


OR


    public class JwtParserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;

        public JwtParserMiddleware(RequestDelegate next, IJwtService jwtService, IOptions<JwtSettings> jwtSettings)
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
                    var userClaims = _jwtService.GetClaimsPrincipal(token, _jwtSettings);
                    if (userClaims != null) context.User = userClaims;
                }
                else
                {
                    if (context.User.Claims.Any())
                    {
                        context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/Json";
                        var bytes = Encoding.ASCII.GetBytes(new { isSuccess = false, message = "UnAuthorized Access To Api !. Token Not Sent.", resultCode = 401 }.SerializeToJson());
                        await context.Response.Body.WriteAsync(bytes);
                    }
                }

                await _next(context);
            }
            catch (Exception e)
            {
                byte[] bytes;
                if (e.Message.Contains("Lifetime validation failed"))
                {
                    #region Expired Token
                    //var validationTime = _jwtService.GetTokenExpireTime(token, _jwtSettings);
                    bytes = Encoding.UTF8.GetBytes(new 
                    {
                        resultCode = 1001,
                        isSuccess = false,
                        message = "JWT Token Lifetime has Expired. Please Login again!",
                    }.SerializeToJson());
                    #endregion
                }
                else
                {
                    #region Another Exception
                    bytes = Encoding.UTF8.GetBytes(new 
                    {
                        resultCode = 1002,
                        isSuccess = false,
                        message = "Invalid JWT Token signature. Please refresh your token." +
                        Environment.NewLine + e.Message
                    }.SerializeToJson());
                    #endregion
                }

                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/Json";
                await context.Response.Body.WriteAsync(bytes);
            }
        }
    }

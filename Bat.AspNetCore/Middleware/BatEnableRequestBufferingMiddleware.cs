namespace Bat.AspNetCore;

public class BatEnableRequestBufferingMiddleware
{
    private readonly RequestDelegate _next;

    public BatEnableRequestBufferingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();

        await _next(context);
    }
}
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Bat.AspNetCore;

public static class ServiceCollectionExtension
{
    public static void AddBatHttpContextAccessor(this IServiceCollection services)
        => services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    public static void AddBatAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.Cookie.SameSite = SameSiteMode.Lax;
            });
    }

    public static void AddBatAuthentication(this IServiceCollection services, SameSiteMode sameSiteMode = SameSiteMode.Lax,
        string cookieAuthenticationDefaults = CookieAuthenticationDefaults.AuthenticationScheme)
    {
        services.AddAuthentication(cookieAuthenticationDefaults)
            .AddCookie(option =>
            {
                option.Cookie.SameSite = sameSiteMode;
            });
    }

    public static void AddBatAuthentication(this IServiceCollection services, Action<CookieAuthenticationOptions> options,
        string cookieAuthenticationDefaults = CookieAuthenticationDefaults.AuthenticationScheme)
    {
        services.AddAuthentication(cookieAuthenticationDefaults)
            .AddCookie(options);
    }

}
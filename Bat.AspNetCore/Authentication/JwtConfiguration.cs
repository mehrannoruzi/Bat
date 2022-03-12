using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Bat.AspNetCore;

public static class JwtConfiguration
{
    public static void UseBatJwtConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }


    public static void AddBatJwtConfiguration(this IServiceCollection services, JwtSettings jwtSettings)
    {
        var secretKey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.SecretKey) ? jwtSettings.SecretKey : "<-- Mehran@Norouzi|123456789987654321|Mehran@Norouzi -->");
        var encryptionkey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.Encryptionkey) ? jwtSettings.Encryptionkey : "<Mehran@Norouzi>");

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateAudience = !string.IsNullOrEmpty(jwtSettings.Audience), //default : false
                    ValidAudience = jwtSettings.Audience,
                ValidateIssuer = !string.IsNullOrEmpty(jwtSettings.Issuer), //default : false
                    ValidIssuer = jwtSettings.Issuer,
                TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.Validate();
        });
    }

    public static void AddBatJwtConfiguration(this IServiceCollection services, Action<JwtBearerOptions> configureOptions)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(configureOptions);
    }

    public static void AddBatJwtConfiguration(this IServiceCollection services, JwtSettings jwtSettings, JwtBearerEvents jwtBearerEvents)
    {
        var secretKey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.SecretKey) ? jwtSettings.SecretKey : "<-- Mehran@Norouzi|123456789987654321|Mehran@Norouzi -->");
        var encryptionkey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.Encryptionkey) ? jwtSettings.Encryptionkey : "<Mehran@Norouzi>");

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateAudience = !string.IsNullOrEmpty(jwtSettings.Audience), //default : false
                    ValidAudience = jwtSettings.Audience,
                ValidateIssuer = !string.IsNullOrEmpty(jwtSettings.Issuer), //default : false
                    ValidIssuer = jwtSettings.Issuer,
                TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.Validate();
            options.Events = jwtBearerEvents;
        });
    }

}
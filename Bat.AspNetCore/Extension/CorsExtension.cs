using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Bat.AspNetCore
{
    public static class CorsExtension
    {
        public static void AddBatCors(this IServiceCollection services, string policyName, CorsPolicy corsPolicy)
        {
            services.AddCors(option =>
            {
                option.AddPolicy(policyName, corsPolicy);
            });
        }

        public static void AddBatCors(this IServiceCollection services, string policyName, List<string> domains = null, List<string> headers = null, List<string> methods = null)
        {
            var policyBuilder = new CorsPolicyBuilder();
            var corsPolicy = domains == null
                            ? policyBuilder.AllowAnyOrigin()
                            : policyBuilder.WithOrigins(domains.ToArray());
            corsPolicy = headers == null
                            ? policyBuilder.AllowAnyHeader()
                            : policyBuilder.WithHeaders(headers.ToArray());
            corsPolicy = methods == null
                            ? policyBuilder.AllowAnyMethod()
                            : policyBuilder.WithMethods(methods.ToArray());

            services.AddCors(option =>
            {
                option.AddPolicy(policyName, policyBuilder.Build());
            });
        }



        public static void UseBatCrossOriginResource(this IApplicationBuilder app, string policyName)
        {
            app.UseCors(policyName);
        }

        public static void UseBatCrossOriginResource(this IApplicationBuilder app)
        {
            app.UseCors(corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyHeader();
                corsPolicyBuilder.AllowAnyMethod();
                corsPolicyBuilder.AllowAnyOrigin();
            });
        }

        public static void UseBatCrossOriginResource(this IApplicationBuilder app, List<string> domains = null, List<string> headers = null, List<string> methods = null)
        {
            var policyBuilder = new CorsPolicyBuilder();
            var corsPolicy = domains == null
                            ? policyBuilder.AllowAnyOrigin()
                            : policyBuilder.WithOrigins(domains.ToArray());
            corsPolicy = headers == null
                            ? policyBuilder.AllowAnyHeader()
                            : policyBuilder.WithHeaders(headers.ToArray());
            corsPolicy = methods == null
                            ? policyBuilder.AllowAnyMethod()
                            : policyBuilder.WithMethods(methods.ToArray());

            app.UseCors(corsPolicyBuilder => corsPolicyBuilder = corsPolicy);
        }
    }
}
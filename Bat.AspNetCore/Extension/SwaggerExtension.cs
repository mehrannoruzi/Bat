using System;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;

namespace Bat.AspNetCore
{
    public static class SwaggerExtension
    {
        public static void AddBatSwagger(this IServiceCollection services, SwaggerSetting swaggerSetting)
        {
            services.AddSwaggerGen(option =>
            {
                //option.DescribeAllEnumsAsStrings();

                option.SwaggerDoc(swaggerSetting.Name.Replace(" ", ""), new OpenApiInfo
                {
                    Title = swaggerSetting.Title,
                    Version = swaggerSetting.Version,
                    Description = swaggerSetting.Description,
                    TermsOfService = new Uri(swaggerSetting.TermsOfService),
                    Contact = new OpenApiContact
                    {
                        Url = new Uri(swaggerSetting.Contact.Url),
                        Name = swaggerSetting.Contact.Name,
                        Email = swaggerSetting.Contact.Email,
                    },
                    License = new OpenApiLicense
                    {
                        Url = new Uri(swaggerSetting.License.Url),
                        Name = swaggerSetting.License.Name,
                    }
                });
            });
        }

        public static void AddBatSwagger(this IServiceCollection services, Action<SwaggerGenOptions> SwaggerOption)
        {
            services.AddSwaggerGen(SwaggerOption);
        }

        public static void AddBatSwagger(this IServiceCollection services, OpenApiInfo swaggerDocInfo, string name)
        {
            services.AddSwaggerGen(option =>
            {
                //option.DescribeAllEnumsAsStrings();

                option.SwaggerDoc(name.Replace(" ", ""), swaggerDocInfo);
            });
        }


        public static void UseBatSwaggerConfiguration(this IApplicationBuilder app, string name = "v1", int modelExpandDepth = 2, string routePrefix = "help")
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.DefaultModelExpandDepth(modelExpandDepth);
                option.SwaggerEndpoint($"/swagger/{name.Replace(" ", "")}/swagger.json", "API V1");
                option.RoutePrefix = routePrefix;
            });
        }

        public static void UseBatSwaggerConfiguration(this IApplicationBuilder app, SwaggerSetting swaggerSetting, int modelExpandDepth = 2, string routePrefix = "help")
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.DefaultModelExpandDepth(modelExpandDepth);
                option.SwaggerEndpoint(swaggerSetting.JsonUrl, swaggerSetting.Name);
                option.RoutePrefix = routePrefix;
            });
        }

        public static void UseBatSwaggerConfiguration(this IApplicationBuilder app, Action<SwaggerUIOptions> swaggerUIOptions)
        {
            app.UseSwagger();
            app.UseSwaggerUI(swaggerUIOptions);
        }

    }
}
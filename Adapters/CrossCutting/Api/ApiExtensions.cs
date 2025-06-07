using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;

namespace CrossCutting.Api
{
    [ExcludeFromCodeCoverage]
    public static class ApiExtensions
    {
        public static void RegisterApi(this IServiceCollection service, IConfiguration configuration)
        {
            AddApiVersioningExtension(service);

            AddSwagger(service);

            ConfigureLocalization(service);

            ConfigureControllers(service);

            ConfigureHttpClient(service);
        }

        private static void AddApiVersioningExtension(this IServiceCollection services)
        {
            _ = services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            })
            .AddApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            _ = services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Weather integration API",
                    Description = "Weather API"
                });

                s.DescribeAllParametersInCamelCase();
            });
        }

        private static void ConfigureLocalization(this IServiceCollection services)
        {
            _ = services.Configure<RequestLocalizationOptions>(
            o =>
            {
                var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pt-BR"),
                };

                o.DefaultRequestCulture = new RequestCulture("en-US");

                o.SupportedCultures = supportedCultures;

                o.SupportedUICultures = supportedCultures;
            });
        }

        private static void ConfigureHttpClient(this IServiceCollection services)
        {
            _ = services.AddHttpClient();
        }

        private static void ConfigureControllers(this IServiceCollection services)
        {
            _ = services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
        }
    }
}
using Application.Middleware;
using CrossCutting;

System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

var configuration = webApplicationBuilder.Configuration;

webApplicationBuilder.Services.AddApiVersioning();

webApplicationBuilder.Services.AddHealthChecks();

webApplicationBuilder.Services.AddEndpointsApiExplorer();

webApplicationBuilder.Services.RegisterDependencies(webApplicationBuilder.Configuration);

webApplicationBuilder.Services.AddSwaggerGenNewtonsoftSupport();

webApplicationBuilder.Services.AddLocalization();

var webApplication = webApplicationBuilder.Build();

var logger = webApplication.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Connection String: {ConnectionString}", configuration.GetConnectionString("MySQLConnection"));

if (webApplication.Environment.IsDevelopment())
{
    webApplication.UseSwagger();

    webApplication.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather integration API");
        c.ShowExtensions();
    });
}

webApplication.UseHttpsRedirection();

//webApplication.UseAuthentication();

//webApplication.UseAuthorization();

webApplication.UseMiddleware<ApiMiddleware>();

webApplication.UseHealthChecks("/health");

webApplication.MapControllers();

var supportedCultures = new[] { "pt-BR", "en-US", "es-ES" };

var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                                                          .AddSupportedCultures(supportedCultures)
                                                          .AddSupportedUICultures(supportedCultures);

webApplication.UseRequestLocalization(localizationOptions);

var startup = new Api.Startup(webApplication.Services);

webApplication.Run();
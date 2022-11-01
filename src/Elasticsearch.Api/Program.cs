using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

var configuration = builder.Configuration;
configuration.AddEnvironmentVariables();

var services = builder.Services;

services.AddOptions();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    // endpoints.MapHealthChecks();
    // endpoints.MapMetrics();
    endpoints.MapControllers();
});


app.Run();
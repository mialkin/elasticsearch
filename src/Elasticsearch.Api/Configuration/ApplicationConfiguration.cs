using MediatR;

namespace Elasticsearch.Api.Configuration;

public static class ApplicationConfiguration
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Program));
    }
}
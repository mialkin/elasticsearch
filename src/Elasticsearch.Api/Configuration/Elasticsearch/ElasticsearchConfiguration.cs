using Ardalis.GuardClauses;
using Elasticsearch.Api.ExtensionMethods;
using Microsoft.Extensions.Options;
using Nest;

namespace Elasticsearch.Api.Configuration.Elasticsearch;

public static class ElasticsearchConfiguration
{
    public static void ConfigureElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSettings<ElasticsearchSettings>(configuration);

        services.AddSingleton<IElasticClient>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;

            Guard.Against.NullOrWhiteSpace(settings.Host);
            var connectionSettings = new ConnectionSettings(new Uri(settings.Host)).DefaultIndex("bogus");

            return new ElasticClient(connectionSettings);
        });
    }
}
using Ardalis.GuardClauses;
using Elasticsearch.Api.ExtensionMethods;
using Elasticsearch.Net;
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

            Guard.Against.NullOrWhiteSpace(settings.Hosts);
            Guard.Against.Null(settings.Port);
            Guard.Against.Null(settings.EnableSsl);

            var hosts = settings.Hosts
                .Split(separator: ',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var uris = hosts
                .Select(host => new UriBuilder
                {
                    Scheme = settings.EnableSsl ?? false ? Uri.UriSchemeHttps : Uri.UriSchemeHttp,
                    Host = host,
                    Port = settings.Port.Value,
                    UserName = settings.Username,
                    Password = settings.Password
                }.Uri);

            var connectionPool = new StaticConnectionPool(uris);
            var connectionSettings = new ConnectionSettings(connectionPool)
                .DefaultIndex(DefaultIndices.Bogus)
                .EnableDebugMode();

            return new ElasticClient(connectionSettings);
        });
    }
}
namespace Elasticsearch.Api.Configuration.Elasticsearch;

public class ElasticsearchSettings
{
    public string? Hosts { get; init; }

    public int? Port { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
    public bool? EnableSsl { get; init; }
}
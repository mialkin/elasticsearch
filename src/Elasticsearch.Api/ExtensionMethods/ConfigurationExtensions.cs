namespace Elasticsearch.Api.ExtensionMethods;

public static class ConfigurationExtensions
{
    public static T GetRequiredSection<T>(this IConfiguration configuration)
    {
        return configuration.GetRequiredSection(typeof(T).Name).Get<T>();
    }

    public static void ConfigureSettings<T>(this IServiceCollection serviceCollection, IConfiguration configuration)
        where T : class
    {
        serviceCollection.Configure<T>(configuration.GetRequiredSection(typeof(T).Name));
    }
}
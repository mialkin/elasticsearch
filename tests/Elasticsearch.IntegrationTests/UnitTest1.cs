using System;
using Nest;
using Xunit;

namespace Elasticsearch.IntegrationTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var client = new ElasticClient();

        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("people");
    }
}
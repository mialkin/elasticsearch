using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Elasticsearch.Api.Index;

[Route("api/index")]
public class IndexController : ControllerBase
{
    private readonly IElasticClient _elasticClient;

    public IndexController(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    [HttpGet("list-all")]
    public async Task<IActionResult> List()
    {
        // GET /_cat/indices?v
        var result = await _elasticClient.Indices.GetAsync(new GetIndexRequest(Indices.All));
        var indexNames = result.Indices.Select(x => x.Key.Name);
        return Ok(indexNames);
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(string indexName)
    {
        // GET indexName
        var result = await _elasticClient.Indices.GetAsync(indexName);
        var index = result.Indices.First();
        var name = index.Key.Name;
        var settings = index.Value.Settings;
        var mappings = index.Value.Mappings;
        var aliases = index.Value.Aliases;
        return Ok(new { name, aliases, mappings, settings });
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(string indexName)
    {
        await _elasticClient.Indices.CreateAsync(indexName);
        return Ok();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(string indexName)
    {
        // DELETE indexName
        await _elasticClient.Indices.DeleteAsync(indexName);
        return Ok();
    }
}
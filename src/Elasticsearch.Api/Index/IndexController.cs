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
        var result = await _elasticClient.Indices.GetAsync(new GetIndexRequest(Indices.All));
        var indexNames = result.Indices.Select(x => x.Key.Name);
        return Ok(indexNames);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(string indexName)
    {
        await _elasticClient.Indices.CreateAsync(indexName);
        return Ok();
    }
}
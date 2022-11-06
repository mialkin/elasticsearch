using Elasticsearch.Api.Bogus.Commands;
using Elasticsearch.Api.Bogus.Dtos;
using Elasticsearch.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Elasticsearch.Api.Bogus;

[Route("api/bogus")]
public class BogusController : ApiControllerBase
{
    private readonly IElasticClient _elasticClient;

    public BogusController(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }
    
    [HttpPost]
    public async Task<IActionResult> Index([FromBody] IndexBogusDto dto)
    {
        await Mediator.Send(new IndexBogusDtoCommand(dto));
        
        return Ok();
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> Search()
    {
        var result = await _elasticClient.SearchAsync<IndexBogusDto>();
        return Ok(result);
    }
}
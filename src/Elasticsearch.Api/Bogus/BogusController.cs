using Elasticsearch.Api.Bogus.Dtos;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Elasticsearch.Api.Bogus;

[Route("api/bogus")]
public class BogusController : ControllerBase
{
    private readonly IElasticClient _elasticClient;

    public BogusController(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    [HttpPost]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var document = new IndexBogusDto("The title", Random.Shared.Next(300));
        await _elasticClient.IndexDocumentAsync(document, cancellationToken);
        return Ok();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(CancellationToken cancellationToken)
    {
        var response =
            await _elasticClient.SearchAsync<IndexBogusDto>(x =>
                    x.Size(100).Query(y => y.MatchAll()),
                cancellationToken);

        return Ok(response.Documents);
    }
}
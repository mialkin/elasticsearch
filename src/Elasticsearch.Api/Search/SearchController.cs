using Elasticsearch.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Elasticsearch.Api.Search;

[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly IElasticClient _elasticClient;

    public SearchController(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    [HttpGet("match-all")]
    public async Task<IActionResult> MathAll()
    {
        // GET products/_search
        // or:
        // GET products/_search
        // {
        //     "query": {
        //         "match_all": {}
        //     }
        // }
        var result = await _elasticClient.SearchAsync<ProductDto>(x => x.Index("products").MatchAll());
        var documents = result.Documents;
        return Ok(documents);
    }

    [HttpGet("term-price")]
    public async Task<IActionResult> TermPrice(decimal price)
    {
        // GET products/_search
        // {
        //     "query": {
        //         "term": {
        //             "price": {
        //                 "value": 19.99
        //             }
        //         }
        //     }
        // }
        var result = await _elasticClient.SearchAsync<ProductDto>(search =>
            search
                .Index("products")
                .Query(query =>
                    query.Term(x => x.Field(y => y.Price).Value(price)))
        );

        var documents = result.Documents;
        return Ok(documents);
    }
}
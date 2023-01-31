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

    [HttpGet("term-prices")]
    public async Task<IActionResult> TermPrices(IEnumerable<decimal> prices)
    {
        // GET products/_search
        // {
        //     "query": {
        //         "terms": {
        //             "price": [
        //             19.99,
        //             10.99
        //                 ]
        //         }
        //     }
        // }
        var result = await _elasticClient.SearchAsync<ProductDto>(search =>
            search
                .Index("products")
                .Query(query =>
                    query.Terms(x => x.Field(y => y.Price).Terms(prices)))
        );

        var documents = result.Documents;
        return Ok(documents);
    }

    [HttpGet("term-prices-and-name")]
    public async Task<IActionResult> TermPricesAndName(IEnumerable<decimal> prices, string name)
    {
        // GET products/_search
        // {
        //     "query": {
        //         "bool": {
        //             "must": [
        //             {
        //                 "terms": {
        //                     "price": [
        //                     19.99,
        //                     10.99
        //                         ]
        //                 }
        //             },
        //             {
        //                 "term": {
        //                     "name": {
        //                         "value": "pillow"
        //                     }
        //                 }
        //             }
        //             ]
        //         }
        //     }
        // }
        var result = await _elasticClient.SearchAsync<ProductDto>(search =>
            search
                .Index("products")
                .Query(query => query
                    .Terms(x => x.Field(y => y.Price).Terms(prices)) && query
                    .Term(x => x.Field(y => y.Name).Value(name)))
        );

        var documents = result.Documents;
        return Ok(documents);
    }
    
    [HttpGet("term-prices-or-name")]
    public async Task<IActionResult> TermPricesOrName(IEnumerable<decimal> prices, string name)
    {
        // GET products/_search
        // {
        //     "query": {
        //         "bool": {
        //             "should": [
        //             {
        //                 "terms": {
        //                     "price": [
        //                     19.99
        //                         ]
        //                 }
        //             },
        //             {
        //                 "term": {
        //                     "name": {
        //                         "value": "chair"
        //                     }
        //                 }
        //             }
        //             ]
        //         }
        //     }
        // }
        var result = await _elasticClient.SearchAsync<ProductDto>(search =>
            search
                .Index("products")
                .Query(query => query
                    .Terms(x => x.Field(y => y.Price).Terms(prices)) || query
                    .Term(x => x.Field(y => y.Name).Value(name)))
        );

        var documents = result.Documents;
        return Ok(documents);
    }
}
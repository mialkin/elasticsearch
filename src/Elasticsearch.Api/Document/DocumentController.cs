using Elasticsearch.Api.Configuration.Elasticsearch;
using Elasticsearch.Api.Dtos;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Elasticsearch.Api.Document;

[Route("api/document")]
public class DocumentController : ControllerBase
{
    private readonly IElasticClient _elasticClient;

    public DocumentController(IElasticClient elasticClient) => _elasticClient = elasticClient;

    [HttpPost("create")]
    public async Task<IActionResult> Create(ProductDto document)
    {
        await _elasticClient.IndexAsync(document, x => x.Refresh(Refresh.WaitFor).Index(DefaultIndices.Products));
        return Ok();

        // POST products/_doc
        // {
        // }
        // or:
        // PUT products/_doc/bd971fd3-9a7b-4202-a2a4-5d143242d453
        // {
        //     "id": "bd971fd3-9a7b-4202-a2a4-5d143242d453",
        //     "name": "chair",
        //     "isActive": true,
        //     "price": 19.99,
        //     "createdOn": "2023-02-13T00:00:00"
        // }
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _elasticClient.GetAsync<ProductDto>(id, x => x.Index(DefaultIndices.Products));
        var dto = result.Source;
        return Ok(dto);

        // GET products/_doc/id
    }

    [HttpPost("delete-by-id")]
    public async Task<IActionResult> Delete(string id)
    {
        await _elasticClient.DeleteAsync(new DeleteRequest(DefaultIndices.Products, id));
        return Ok();

        // DELETE products/_doc/id
    }
}
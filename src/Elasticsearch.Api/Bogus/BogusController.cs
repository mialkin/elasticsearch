using Elasticsearch.Api.Bogus.Commands;
using Elasticsearch.Api.Bogus.Dtos;
using Elasticsearch.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.Api.Bogus;

[Route("api/bogus")]
public class BogusController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Index([FromBody] IndexBogusDto dto)
    {
        await Mediator.Send(new IndexBogusDtoCommand(dto));
        
        return Ok();
    }
}
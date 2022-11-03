using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.Api.Infrastructure;

[ApiController]
[Route("[controller]")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}

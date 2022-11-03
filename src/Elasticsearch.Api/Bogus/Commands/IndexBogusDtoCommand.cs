using Elasticsearch.Api.Bogus.Dtos;
using MediatR;
using Nest;

namespace Elasticsearch.Api.Bogus.Commands;

internal record IndexBogusDtoCommand(IndexBogusDto Dto) : MediatR.IRequest<Unit>;

internal class IndexBogusDtoCommandHandler : IRequestHandler<IndexBogusDtoCommand, Unit>
{
    private readonly IElasticClient _elasticClient;

    public IndexBogusDtoCommandHandler(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }
    
    public async Task<Unit> Handle(IndexBogusDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _elasticClient.SearchAsync<IndexBogusDto>(ct: cancellationToken);
        
        return Unit.Value;
    }
}

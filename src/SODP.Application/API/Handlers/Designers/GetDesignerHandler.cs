using AutoMapper;
using MediatR;
using SODP.Application.API;
using SODP.Application.API.Requests.Designers;
using SODP.DataAccess.CQRS;
using SODP.DataAccess.CQRS.Queries.Designers;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Designers;

public class GetDesignerHandler : IRequestHandler<GetDesignerRequest, ApiResponse<DesignerDTO>>
{
	private readonly IQueryExecutor _queryExecutor;
	private readonly IMapper _mapper;

	public GetDesignerHandler(
		IQueryExecutor queryExecutor,
		IMapper mapper)
    {
		_queryExecutor = queryExecutor;
		_mapper = mapper;
	}
    public async Task<ApiResponse<DesignerDTO>> Handle(GetDesignerRequest request, CancellationToken cancellationToken)
	{
		var query = new GetDesignerQuery(request.Id);
		var designer = await _queryExecutor.ExecuteAsync(query, cancellationToken);
		
		return ApiResponse.Success(_mapper.Map<DesignerDTO>(designer));
	}
}

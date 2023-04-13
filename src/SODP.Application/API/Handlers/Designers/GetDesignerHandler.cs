using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Designers;
using SODP.DataAccess.CQRS.Queries;
using SODP.DataAccess.CQRS.Queries.Common;
using SODP.DataAccess.CQRS.Queries.Designers;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
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

    public async Task<ApiResponse<DesignerDTO>> Handle(
		GetDesignerRequest request, 
		CancellationToken cancellationToken)
	{
		var query = new GetByIdQuery<Designer>(request.Id);
		var designer = await _queryExecutor.ExecuteAsync(query, cancellationToken);
		
		if(designer is null)
		{
			throw new NotFoundException("Designer");
		}

		return ApiResponse.Success(_mapper.Map<DesignerDTO>(designer));
	}
}

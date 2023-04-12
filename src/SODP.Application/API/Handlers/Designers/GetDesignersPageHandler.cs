using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Designers;
using SODP.DataAccess.CQRS.Queries;
using SODP.DataAccess.CQRS.Queries.Designers;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public sealed class GetDesignersPageHandler : IRequestHandler<GetDesignersPageRequest, ApiResponse<Page<DesignerDTO>>>
{
	private readonly IQueryExecutor _queryExecutor;
	private readonly IMapper _mapper;

	public GetDesignersPageHandler(
		IQueryExecutor queryExecutor,
        IMapper mapper)
    {
		_queryExecutor = queryExecutor;
		_mapper = mapper;
	}

    public async Task<ApiResponse<Page<DesignerDTO>>> Handle(
        GetDesignersPageRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDesignersPageQuery(
            request.ActiveStatus,
            request.PageNumber,
            request.PageSize,
            request.SearchString);

        var designersPage = await _queryExecutor.ExecuteAsync(query, cancellationToken);

        return ApiResponse.Success(_mapper.Map<Page<DesignerDTO>>(designersPage));
    }
}

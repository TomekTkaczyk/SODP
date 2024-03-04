using MediatR;
using SODP.Application.API.Requests.Designers;
using SODP.Application.Extensions;
using SODP.Application.Mappers;
using SODP.Application.Specifications.Designers;
using SODP.Domain.Attributes;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public sealed class GetDesignersPageHandler : IRequestHandler<GetDesignersPageRequest, ApiResponse<Page<DesignerDTO>>>
{
	private readonly IDesignerRepository _designerRepository;

	public GetDesignersPageHandler(IDesignerRepository designerRepository)
    {
		_designerRepository = designerRepository;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<Page<DesignerDTO>>> Handle(
        GetDesignersPageRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new DesignerSearchSpecification(
                request.ActiveStatus,
                request.SearchString);

        var mapCache = new List<object>();

        var page = await _designerRepository
            .Get(specification)
            .Select(x => x.ToDTO(mapCache))
            .AsPageAsync(request.PageNumber, request.PageSize, cancellationToken);

        return ApiResponse.Success(page);
    }
}

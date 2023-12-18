using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Designers;
using SODP.Application.Specifications.Designers;
using SODP.Domain.Attributes;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public sealed class GetDesignersPageHandler : IRequestHandler<GetDesignersPageRequest
    , ApiResponse<Page<DesignerDTO>>>
{
	private readonly IDesignerRepository _designerRepository;
	private readonly IMapper _mapper;

	public GetDesignersPageHandler(
		IDesignerRepository designerRepository,
        IMapper mapper)
    {
		_designerRepository = designerRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<Page<DesignerDTO>>> Handle(
        GetDesignersPageRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new DesignerSearchSpecification(
                request.ActiveStatus,
                request.SearchString);

        var page = await _designerRepository
            .GetPageAsync(
                specification,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

        return ApiResponse.Success(_mapper.Map<Page<DesignerDTO>>(page));
    }
}

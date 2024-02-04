using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Parts;
using SODP.Application.Extensions;
using SODP.Application.Specifications.Parts;
using SODP.Domain.Attributes;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Parts;

public sealed class GetPartsPageHandler : IRequestHandler<GetPartsPageRequest, ApiResponse<Page<PartDTO>>>
{
	private readonly IPartRepository _partRepository;
	private readonly IMapper _mapper;

	public GetPartsPageHandler(
		IPartRepository partRepository,
		IMapper mapper)
	{
		_partRepository = partRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<Page<PartDTO>>> Handle(
		GetPartsPageRequest request,
		CancellationToken cancellationToken)
	{
		var specification = new PartSearchSpecification(
				request.ActiveStatus,
				request.SearchString);

		var page = await _partRepository
			.Get(specification)
			.AsPageAsync(request.PageNumber, request.PageSize, cancellationToken);

		return ApiResponse.Success(_mapper.Map<Page<PartDTO>>(page));
	}
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Parts;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Parts;

public class GetPartHandler : IRequestHandler<GetPartRequest, ApiResponse<PartDTO>>
{
	private readonly IPartRepository _partRepository;
	private readonly IMapper _mapper;

	public GetPartHandler(
		IPartRepository partRepository,
		IMapper mapper)
	{
		_partRepository = partRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<PartDTO>> Handle(GetPartRequest request, CancellationToken cancellationToken)
	{
		var part = await _partRepository
			.Get(new ByIdSpecification<Part>(request.Id))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("Part");

		return ApiResponse.Success(_mapper.Map<PartDTO>(part));
	}
}

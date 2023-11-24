using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Designers;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Designers;

public sealed class GetDesignerHandler : IRequestHandler<GetDesignerRequest, ApiResponse<DesignerDTO>>
{
	private readonly IDesignerRepository _designerRepository;
	private readonly IMapper _mapper;

	public GetDesignerHandler(
		IDesignerRepository designerRepository,
		IMapper mapper)
    {
		_designerRepository = designerRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<DesignerDTO>> Handle(
		GetDesignerRequest request, 
		CancellationToken cancellationToken)
	{
		var designer = await _designerRepository
			.Get(new ByIdSpecification<Designer>(request.Id))
			.SingleOrDefaultAsync(cancellationToken) 
			?? throw new NotFoundException("Designer");

		return ApiResponse.Success(_mapper.Map<DesignerDTO>(designer));
	}
}

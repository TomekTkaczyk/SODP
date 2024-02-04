using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Mappers;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetPartHandler : IRequestHandler<GetPartRequest, ApiResponse<ProjectPartDTO>>
{
	private readonly IProjectPartRepository _projectPartRepository;
	private readonly IMapper _mapper;

	public GetPartHandler(
		IProjectPartRepository projectPartRepository,
		IMapper mapper)
    {
		_projectPartRepository = projectPartRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<ProjectPartDTO>> Handle(GetPartRequest request, CancellationToken cancellationToken)
	{
		var specification = new ByIdSpecification<ProjectPart>(request.ProjectPartId);
		
		var projectPart = await _projectPartRepository
			.Get(specification)
			.SingleOrDefaultAsync(cancellationToken);


        return ApiResponse.Success(projectPart.ToDTO());
	}
}

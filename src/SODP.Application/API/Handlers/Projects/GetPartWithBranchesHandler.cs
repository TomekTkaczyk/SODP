using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetPartWithBranchesHandler : IRequestHandler<GetPartWithBranchesRequest, ApiResponse<ICollection<PartBranchDTO>>>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IMapper _mapper;

	public GetPartWithBranchesHandler(
        IProjectRepository projectRepository,
		IMapper mapper)
    {
		_projectRepository = projectRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<ICollection<PartBranchDTO>>> Handle(GetPartWithBranchesRequest request, CancellationToken cancellationToken)
	{
		var projectPart = await _projectRepository.GetPartAsync(request.ProjectPartId, cancellationToken);


		return ApiResponse.Success(_mapper.Map<ICollection<PartBranchDTO>>(projectPart));
	}
}

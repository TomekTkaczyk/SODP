using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Parts;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetPartHandler : IRequestHandler<GetPartRequest, ApiResponse<ProjectPartDTO>>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IMapper _mapper;

	public GetPartHandler(
        IProjectRepository projectRepository,
		IMapper mapper)
    {
		_projectRepository = projectRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<ProjectPartDTO>> Handle(GetPartRequest request, CancellationToken cancellationToken)
	{
		var projectPart = await _projectRepository.GetPartAsync(request.Id, cancellationToken);


		return ApiResponse.Success(_mapper.Map<ProjectPartDTO>(projectPart));
	}
}

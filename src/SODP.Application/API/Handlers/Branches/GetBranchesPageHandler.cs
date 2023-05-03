using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class GetBranchesPageHandler : IRequestHandler<GetBranchesPageRequest, ApiResponse<Page<BranchDTO>>>
{
    private readonly IBranchRepository _branchRepository;
	private readonly IMapper _mapper;

	public GetBranchesPageHandler(
         IBranchRepository branchRepository,
         IMapper mapper)
    {
        _branchRepository = branchRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<Page<BranchDTO>>> Handle(
        GetBranchesPageRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new BranchSearchSpecification(
                request.ActiveStatus,
                request.SearchString);

        var page = await _branchRepository.GetPageAsync(
            specification,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return ApiResponse.Success(_mapper.Map<Page<BranchDTO>>(page));
    }
}

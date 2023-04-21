using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Branches;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class GetBranchByIdWithLicensesHandler : IRequestHandler<GetBranchWithLicensesRequest, ApiResponse<BranchDTO>>
{
    private readonly IBranchRepository _branchRepository;
	private readonly IMapper _mapper;

	public GetBranchByIdWithLicensesHandler(
            IBranchRepository branchRepository,
            IMapper mapper)
    {
        _branchRepository = branchRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<BranchDTO>> Handle(
        GetBranchWithLicensesRequest request,
        CancellationToken cancellationToken)
    {
        var branch = await _branchRepository
            .GetByIdWithDetailsAsync(request.Id, cancellationToken)
			?? throw new NotFoundException("Branch");

        return ApiResponse.Success(_mapper.Map<BranchDTO>(branch));
    }
}

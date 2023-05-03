using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Linq;
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
        var specification = new ByIdSpecification<Branch>(request.Id);
        var branch = await _branchRepository
            .ApplySpecyfication(specification)
            .Include(x => x.Licenses)
			.ThenInclude(s => s.License)
			.ThenInclude(s => s.Designer)
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("Branch");

        return ApiResponse.Success(_mapper.Map<BranchDTO>(branch));
    }
}

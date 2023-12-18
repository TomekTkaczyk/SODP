using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class GetBranchWithLicensesHandler : IRequestHandler<GetBranchWithLicensesRequest, ApiResponse<BranchDTO>>
{
	private readonly IBranchRepository _branchRepository;
	private readonly IMapper _mapper;

	public GetBranchWithLicensesHandler(
			IBranchRepository branchRepository,
			IMapper mapper)
	{
		_branchRepository = branchRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<BranchDTO>> Handle(
		GetBranchWithLicensesRequest request,
		CancellationToken cancellationToken)
	{
		var specification = new ByIdSpecification<Branch>(request.Id);
		var branch = await _branchRepository
			.Get(specification)
			.Include(x => x.Licenses)  
			.ThenInclude(s => s.License)
			.ThenInclude(s => s.Designer)
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("Branch");

		var dto = _mapper.Map<BranchDTO>(branch);

		return ApiResponse.Success(dto);
	}
}
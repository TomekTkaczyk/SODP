using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Licenses;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public class GetLicenseWithBranchesHandler : IRequestHandler<GetLicenseWithBranchesRequest, ApiResponse<LicenseDTO>>
{
	private readonly ILicensesRepository _licensesRepository;
	private readonly IMapper _mapper;

	public GetLicenseWithBranchesHandler(
		ILicensesRepository licensesRepository,
		IMapper mapper)
	{
		_licensesRepository = licensesRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<LicenseDTO>> Handle(
		GetLicenseWithBranchesRequest request, 
		CancellationToken cancellationToken)
	{
		var license = await _licensesRepository
			.Get(new ByIdSpecification<License>(request.Id))
			.Include(x => x.Designer)
			.Include(x => x.Branches)
			.ThenInclude(x => x.Branch)
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("License");

		return ApiResponse.Success(_mapper.Map<LicenseDTO>(license));
	}
}

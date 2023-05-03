using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Licenses;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class GetLicenseHandler : IRequestHandler<GetLicenseRequest, ApiResponse<LicenseDTO>>
{
	private readonly ILicensesRepository _licenseRepository;
	private readonly IMapper _mapper;

	public GetLicenseHandler(
        ILicensesRepository licenseRepository,
        IMapper mapper)
    {
		_licenseRepository = licenseRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<LicenseDTO>> Handle(
		GetLicenseRequest request, 
		CancellationToken cancellationToken)
	{
		var license = await _licenseRepository
			.ApplySpecyfication(new ByIdSpecification<License>(request.Id))
			.Include(x => x.Designer)
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("License");

		return ApiResponse.Success(_mapper.Map<LicenseDTO>(license));
	}
}

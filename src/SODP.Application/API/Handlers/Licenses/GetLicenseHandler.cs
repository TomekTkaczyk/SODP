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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class GetLicenseHandler : IRequestHandler<GetLicenseRequest, ApiResponse<LicenseDTO>>
{
	private readonly ILicensesRepository _licenseRepository;

	public GetLicenseHandler(ILicensesRepository licenseRepository)
    {
		_licenseRepository = licenseRepository;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<LicenseDTO>> Handle(
		GetLicenseRequest request, 
		CancellationToken cancellationToken)
	{
		var license = await _licenseRepository
			.Get(new ByIdSpecification<License>(request.Id))
			.Include(x => x.Designer)
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("License");

		LicenseDTO licenseDTO = new(
			license.Id,
			new DesignerDTO(
				license.Designer.Id,
				license.Designer.Title,
				license.Designer.Firstname,
				license.Designer.Lastname,
				license.Designer.ActiveStatus,
				new List<LicenseDTO>()),
			license.Content,
			new List<BranchDTO>());

		return ApiResponse.Success(licenseDTO);
	}
}

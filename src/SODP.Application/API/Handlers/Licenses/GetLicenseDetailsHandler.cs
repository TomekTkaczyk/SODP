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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public class GetLicenseDetailsHandler : IRequestHandler<GetLicenseDetailsRequest, ApiResponse<LicenseDTO>>
{
	private readonly ILicensesRepository _licensesRepository;

	public GetLicenseDetailsHandler(
		ILicensesRepository licensesRepository)
	{
		_licensesRepository = licensesRepository;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<LicenseDTO>> Handle(
		GetLicenseDetailsRequest request, 
		CancellationToken cancellationToken)
	{
		var license = await _licensesRepository
			.Get(new ByIdSpecification<License>(request.Id))
			.Include(x => x.Designer)
			.Include(x => x.Branches)
			.ThenInclude(x => x.Branch)
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("License");

		var branches = license.Branches.Select(x => new BranchDTO(
				x.BranchId,
				x.Branch.Sign,
				x.Branch.Title,
				x.Branch.Order,
				x.Branch.ActiveStatus,
				new List<LicenseDTO>())).ToList();

		DesignerDTO designerDTO = new(
			license.Designer.Id,
			license.Designer.Title.Value,
			license.Designer.Firstname.Value,
			license.Designer.Lastname.Value,
			license.Designer.ActiveStatus,
			new List<LicenseDTO>());

        LicenseDTO licenseDTO = new(
			license.Id,
			designerDTO,
			license.Content,
            branches);

        return ApiResponse.Success(licenseDTO);
	}
}

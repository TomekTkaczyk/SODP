using MediatR;
using SODP.Application.API.Requests.Licenses;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public class GetLicenseWithBranchesHandler : IRequestHandler<GetLicenseWithBranchesRequest, ApiResponse<LicenseDTO>>
{
	public Task<ApiResponse<LicenseDTO>> Handle(
		GetLicenseWithBranchesRequest request, 
		CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

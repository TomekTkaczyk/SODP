using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Licenses;

public record GetLicenseRequest(
	int Id) : IRequest<ApiResponse<LicenseDTO>>;

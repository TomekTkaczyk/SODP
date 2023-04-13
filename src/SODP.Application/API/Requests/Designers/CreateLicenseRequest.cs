using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Designers;

public record CreateLicenseRequest(
	int DesignerId,
	string Content) : IRequest<ApiResponse<LicenseDTO>>;

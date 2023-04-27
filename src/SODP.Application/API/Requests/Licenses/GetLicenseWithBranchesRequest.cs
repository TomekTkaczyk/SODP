using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Licenses;

public sealed record GetLicenseWithBranchesRequest(int Id) : IRequest<ApiResponse<LicenseDTO>>;

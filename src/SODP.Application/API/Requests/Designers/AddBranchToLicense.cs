using MediatR;

namespace SODP.Application.API.Requests.Designers;

public sealed record AddBranchToLicenseRequest(
	int LicenseId,
	int BranchId) : IRequest;

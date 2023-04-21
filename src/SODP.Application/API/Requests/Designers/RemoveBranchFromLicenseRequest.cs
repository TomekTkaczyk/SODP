using MediatR;

namespace SODP.Application.API.Requests.Designers;

public sealed record RemoveBranchFromLicenseRequest(
	int LicenseId,
	int BranchId) : IRequest;

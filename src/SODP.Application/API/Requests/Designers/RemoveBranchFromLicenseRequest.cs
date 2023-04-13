using MediatR;

namespace SODP.Application.API.Requests.Designers;

public record RemoveBranchFromLicenseRequest(
	int LicenseId,
	int BranchId) : IRequest;

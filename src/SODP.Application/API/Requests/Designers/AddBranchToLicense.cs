using MediatR;

namespace SODP.Application.API.Requests.Designers;

public record AddBranchToLicenseRequest(
	int LicenseId,
	int BranchId) : IRequest;

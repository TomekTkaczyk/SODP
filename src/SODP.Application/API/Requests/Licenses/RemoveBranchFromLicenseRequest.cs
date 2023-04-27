using MediatR;

namespace SODP.Application.API.Requests.Licenses;

public sealed record RemoveBranchFromLicenseRequest(int Id, int BranchId) : IRequest;

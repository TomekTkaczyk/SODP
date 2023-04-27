using MediatR;

namespace SODP.Application.API.Requests.Licenses;

public sealed record AddBranchToLicenseRequest(int Id, int BranchId) : IRequest;

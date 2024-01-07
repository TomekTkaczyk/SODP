using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Licenses;

public sealed record AddBranchToLicenseRequest(int Id, int BranchId) : IRequest;

using MediatR;
using SODP.Shared.Enums;

namespace SODP.Application.API.Requests.Projects;

public sealed record DeleteRoleFromPartBranchRequest(
    int PartBranchId,
    TechnicalRole Role) : IRequest;

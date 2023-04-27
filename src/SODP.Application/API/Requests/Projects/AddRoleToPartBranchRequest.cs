using MediatR;
using SODP.Shared.Enums;

namespace SODP.Application.API.Requests.Projects;

public sealed record AddRoleToPartBranchRequest(int PartBranchId, TechnicalRole Role, int LicenceId) : IRequest;

using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record DeleteRoleFromPartBranchRequest(int BranchRoleId) : IRequest;

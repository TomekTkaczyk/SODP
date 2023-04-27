using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record RemoveBranchFromPartRequest(int PartBranchId) : IRequest;

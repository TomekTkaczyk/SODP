using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record AddBranchToPartRequest(int PartId, int BranchId) : IRequest;

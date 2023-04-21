using MediatR;

namespace SODP.Application.API.Requests.Branches;

public sealed record DeleteBranchRequest(
	int Id) : IRequest;

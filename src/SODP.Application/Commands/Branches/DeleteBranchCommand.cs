using MediatR;

namespace SODP.Application.Commands.Branches;

public sealed record DeleteBranchCommand(int Id) : IRequest;

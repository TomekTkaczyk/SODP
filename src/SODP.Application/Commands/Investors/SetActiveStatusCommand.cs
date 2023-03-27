using SODP.Application.Abstractions;

namespace SODP.Application.Commands.Investors;

public sealed record SetActiveStatusCommand(int Id, bool ActiveStatus) : ICommand;

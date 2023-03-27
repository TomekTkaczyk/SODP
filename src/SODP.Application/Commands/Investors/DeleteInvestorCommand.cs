using SODP.Application.Abstractions;

namespace SODP.Application.Commands.Investors;

public sealed record DeleteInvestorCommand(int Id) : ICommand;

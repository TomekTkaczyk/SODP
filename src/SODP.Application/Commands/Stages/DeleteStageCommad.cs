using SODP.Application.Abstractions;

namespace SODP.Application.Commands.Stages;

public record DeleteStageCommad(int Id) : ICommand;


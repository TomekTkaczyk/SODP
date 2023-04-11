using MediatR;

namespace SODP.Application.Commands.Stages;

public record DeleteStageCommad(int Id) : IRequest;


using MediatR;

namespace SODP.Application.Commands.Designers;

public record DeleteDesignerCommand(int Id) : IRequest;

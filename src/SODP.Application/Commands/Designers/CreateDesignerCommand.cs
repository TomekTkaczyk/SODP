using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Commands.Designers;

public record CreateDesignerCommand(
	string Title,
	string Firstname,
	string Lastname) : IRequest<Designer>;

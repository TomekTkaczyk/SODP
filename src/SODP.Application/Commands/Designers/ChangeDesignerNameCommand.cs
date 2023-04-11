using MediatR;

namespace SODP.Application.Commands.Designers;

public record ChangeDesignerNameCommand(
	int Id,
	string Title,
	string Firstname,
	string Lastname) : IRequest;

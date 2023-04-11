using MediatR;

namespace SODP.Application.API.Requests.Designers;

public record DeleteDesignerRequest(
	int Id) : IRequest;

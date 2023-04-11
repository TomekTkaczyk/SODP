using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Designers;

public record GetDesignerByIdRequest(
	int Id) : IRequest<Designer>;

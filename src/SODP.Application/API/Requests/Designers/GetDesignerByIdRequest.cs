using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Designers;

public sealed record GetDesignerByIdRequest(
	int Id) : IRequest<Designer>;

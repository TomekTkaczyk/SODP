using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Designers;

public record GetDesignerWithDetailsRequest(
	int DesignerId) : IRequest<Designer>;

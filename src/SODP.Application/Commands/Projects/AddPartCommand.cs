using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.DTO;

namespace SODP.Application.Commands.Projects;

public sealed record AddPartCommand(
	int Id,
	PartDTO part) : IRequest<Part>;

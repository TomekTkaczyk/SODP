using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Stages;

public record GetStageByIdRequest(
	int Id) : IRequest<Stage>;

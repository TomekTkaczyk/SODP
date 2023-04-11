using MediatR;

namespace SODP.Application.API.Requests.Stages;

public record DeleteStageRequest(
	int Id) : IRequest;


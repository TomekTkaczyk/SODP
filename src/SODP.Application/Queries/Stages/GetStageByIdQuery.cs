using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Queries.Stages;

public record GetStageByIdQuery(int Id) : IRequest<Stage> { }

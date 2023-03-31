using SODP.Application.Abstractions;
using SODP.Shared.DTO;

namespace SODP.Application.Queries.Stages;

public record GetStageByIdQuery(int Id) : IQuery<StageDTO>
{
}

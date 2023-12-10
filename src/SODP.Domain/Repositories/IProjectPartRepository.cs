using SODP.Domain.Entities;

namespace SODP.Domain.Repositories;

public interface IProjectPartRepository : IQueryableRepository<ProjectPart>
{
    void Delete(ProjectPart projectPart);
}

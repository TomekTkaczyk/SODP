using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Domain.Shared.Specifications;

namespace SODP.Infrastructure.Repositories;

public class ProjectPartRespository : IProjectPartRepository
{
    private readonly SODPDBContext _dbContext;
    private readonly IQueryable<ProjectPart> _entities;

    public ProjectPartRespository(SODPDBContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<ProjectPart>();
    }

    public void Delete(ProjectPart entity)
    {
        _dbContext.Entry(entity).State = EntityState.Deleted;
    }

    public IQueryable<ProjectPart> Get(ISpecification<ProjectPart> specification = null) =>
        SpecificationEvaluator<ProjectPart>.GetQuery(_entities, specification);
}

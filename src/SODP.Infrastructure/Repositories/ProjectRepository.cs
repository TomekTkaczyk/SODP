using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Infrastructure.Repositories
{
	public class ProjectRepository : PagedRepository<Project>, IProjectRepository
	{
		public ProjectRepository(SODPDBContext dbContext) : base(dbContext) { }

		public async Task<Page<Project>> GetPageAsync(
			ProjectStatus status, 
			string searchString, 
			int pageNumber, 
			int pageSize, 
			CancellationToken cancellationToken)
		{
			var specyfication = new ProjectByNameSpecyfication(status, searchString);
			var queryable = ApplySpecyfication(specyfication);
			var totalItems = await queryable.CountAsync(cancellationToken);

			if(pageSize > 0)
			{
				queryable = GetPageQuery(queryable, pageNumber, pageSize);
			}

			var collection = await queryable.ToListAsync(cancellationToken);

			return Page<Project>.Create(
				collection,
				pageNumber,
				pageSize,
				totalItems); 
		}
	}
}

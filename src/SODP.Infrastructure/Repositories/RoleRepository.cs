using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
	private readonly RoleManager<Role> _roleManager;

	public RoleRepository(RoleManager<Role> roleManager)
	{
		_roleManager = roleManager;
	}


	public async Task<Page<Role>> GetPageAsync(
		bool? activeStatus, 
		int pageNumber, 
		int pageSize, 
		CancellationToken cancellationToken)
	{
		var queryable = _roleManager.Roles
			.Where(x => !activeStatus.HasValue)
			.OrderBy(x => x.Name);
		var totalItems = await queryable.CountAsync(cancellationToken);

		var collection = await GetPageQuery(queryable, pageNumber, pageSize).ToListAsync(cancellationToken);

		return Page<Role>.Create(collection, pageNumber, pageSize, totalItems);
	}


	private static IQueryable<Role> GetPageQuery(
		IQueryable<Role> query, 
		int pageNumber, 
		int pageSize)
	{
		if (pageNumber < 1)
		{
			throw new ArgumentOutOfRangeException(nameof(pageNumber), "Error: Required pageNumber > 0");
		}

		if (query is IOrderedQueryable<Role>)
		{
			query = (query as IOrderedQueryable<Role>).ThenBy(x => x.Id);
		}
		else
		{
			query = query.OrderBy(x => x.Id);
		}

		if (pageSize > 0)
		{
			query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
		}

		return query.AsNoTracking();
	}

}

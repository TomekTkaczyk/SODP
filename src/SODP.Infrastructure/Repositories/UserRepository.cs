using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private readonly UserManager<User> _userManager;

	public IQueryable<User> Users => _userManager.Users;

	public UserRepository(UserManager<User> userManager)
	{
		_userManager = userManager;
	}


	public async Task<Page<User>> GetPageAsync(bool? activeStatus, string searchString, int pageNumber, int pageSize, CancellationToken cancellationToken)
	{
		var queryable = _userManager.Users
			.Where(x => !activeStatus.HasValue)
			.OrderBy(x => x.UserName);
		var totalItems = await queryable.CountAsync(cancellationToken);

		var collection = GetPageQuery(queryable, pageNumber, pageSize);

		return new Page<User>(pageNumber, pageSize, totalItems, collection);
	}
	

	private static IQueryable<User> GetPageQuery(
	IQueryable<User> query,
	int pageNumber,
	int pageSize)
	{
		if (pageNumber < 1)
		{
			throw new ArgumentOutOfRangeException(nameof(pageNumber), "Error: Required pageNumber > 0");
		}

		if (query is IOrderedQueryable<User>)
		{
			query = (query as IOrderedQueryable<User>).ThenBy(x => x.Id);
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

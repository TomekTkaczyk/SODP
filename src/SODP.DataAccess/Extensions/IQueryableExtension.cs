using System.Linq;

namespace SODP.DataAccess.Extensions;

public static class IQueryableExtension
{
	public static IQueryable<T> GetPageQuery<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
	{
		return queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);
	}
}

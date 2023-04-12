using Microsoft.EntityFrameworkCore;
using SODP.DataAccess.Extensions;
using SODP.Domain.Entities;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Queries.Designers;

public class GetDesignersPageQuery : QueryBase<Page<Designer>>
{
	private readonly bool? _active;
	private readonly int _pageNumber;
	private readonly int _pageSize;
	private readonly string _searchString;

	public GetDesignersPageQuery(
		bool? active,
		int pageNumber,
		int pageSize,
		string searchString)
	{
		_active = active;
		_pageNumber = pageNumber;
		_pageSize = pageSize;
		_searchString = searchString;
	}

	public override async Task<Page<Designer>> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken)
	{
		IQueryable<Designer> queryable = context.Set<Designer>()
			.Where(designer =>
				(!_active.HasValue || designer.ActiveStatus.Equals(_active)) &&
				(string.IsNullOrWhiteSpace(_searchString)
				|| designer.Firstname.ToLower().Contains(_searchString.ToLower())
				|| designer.Lastname.ToLower().Contains(_searchString.ToLower())))
			.OrderBy(x => x.Lastname)
			.ThenBy(x => x.Firstname);

		var totalCount = await queryable.CountAsync(cancellationToken);

		if(_pageSize > 0)
		{
			queryable = queryable.GetPageQuery(_pageNumber, _pageSize);
		}

		var page = Page<Designer>.Create(
				queryable.AsNoTracking().ToList(),
				_pageNumber,
				_pageSize,
				totalCount);

		return page;
	}
}

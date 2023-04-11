using SODP.Domain.Entities;
using SODP.Shared.Response;
using System;
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

	public override Task<Page<Designer>> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

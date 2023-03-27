using System.Collections.Generic;

namespace SODP.Shared.Response;

public sealed class Page<T>
{
	public IReadOnlyCollection<T> Collection { get; set; }
	
	public int PageNumber { get; set; }

	public int PageSize { get; set; }

	public int TotalCount { get; set; }
}

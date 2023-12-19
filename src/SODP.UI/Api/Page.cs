using System.Collections.Generic;

namespace SODP.UI.Api;

public record Page
{
	public int PageNumber { get; set; }

	public int PageSize { get; set; }

	public int TotalCount { get; set; }

}

public record Page<T> : Page
{
	public ICollection<T> Collection { get; set; }
}
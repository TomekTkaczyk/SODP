using System.Collections.Generic;

namespace SODP.Shared.Response;

public record Page
{
	public int PageNumber { get; set; }

	public int PageSize { get; set; }

	public int TotalCount { get; set; }

	protected Page(int pageNumber, int pageSize, int tootalCount)
    {
		PageNumber = pageNumber;
		PageSize = pageSize;
		TotalCount = tootalCount;
    }
}

public record Page<T> : Page
{
    public Page(IReadOnlyCollection<T> collection, int pageNumber, int pageSize, int tootalCount) : base(pageNumber, pageSize, tootalCount)	
    {
		Collection = collection;
    }

    public IReadOnlyCollection<T> Collection { get; set; }
	
	public static Page<T> Create(IReadOnlyCollection<T> collection, int pageNumber, int pageSize, int tootalCount)
	{
		 return new Page<T>(collection, pageNumber, pageSize, tootalCount);
	}
}

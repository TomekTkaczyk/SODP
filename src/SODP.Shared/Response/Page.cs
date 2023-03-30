using System.Collections.Generic;

namespace SODP.Shared.Response;

public class Page
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

public class Page<T> : Page
{
    public Page(int pageNumber, int pageSize, int tootalCount, IReadOnlyCollection<T> collection) : base(pageNumber, pageSize, tootalCount)	
    {
		Collection = collection;
    }

    public IReadOnlyCollection<T> Collection { get; set; }
	
	public static Page<T> Create(int pageNumber, int pageSize, int tootalCount, IReadOnlyCollection<T> collection)
	{
		 return new Page<T>(pageNumber, pageSize, tootalCount, collection);
	}
}

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
    public Page(ICollection<T> collection, int pageNumber, int pageSize, int tootalCount) : base(pageNumber, pageSize, tootalCount)	
    {
		Collection = collection;
    }

    public ICollection<T> Collection { get; set; }
	
	public static Page<T> Create(ICollection<T> collection, int pageNumber, int pageSize, int tootalCount)
	{
		 return new Page<T>(collection, pageNumber, pageSize, tootalCount);
	}
}

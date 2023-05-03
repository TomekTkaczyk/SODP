using System;

namespace SODP.UI.Api;

public class PageInfo
{
	public int TotalItems { get; set; }
	public int ItemsPerPage { get; set; }
	public int CurrentPage { get; set; }
	public int TotalPages => ItemsPerPage != 0 ? (int)Math.Ceiling(decimal.Divide(TotalItems, ItemsPerPage)) : 1;
	public string RequestParameters => $"pageNumber={CurrentPage}&pageSize={ItemsPerPage}";
	public string Url { get; set; }
}

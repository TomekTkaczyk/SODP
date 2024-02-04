using System.Collections.Generic;

namespace SODP.Shared.Response;

public record Page<T>(
	int PageNumber, 
	int PageSize, 
	int TotalCount,
	IEnumerable<T> Collection);

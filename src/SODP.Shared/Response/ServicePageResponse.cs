using System.Collections.Generic;

namespace SODP.Shared.Response;

public class ServicePageResponse<T> : ServiceResponse<Page<T>> // where T : BaseDTO
{
	public ServicePageResponse()
	{
		Data = Page<T>.Create(new List<T>(), 0, 0, 0);
	}

	public void SetData(IReadOnlyCollection<T> data)
	{
		Data.Collection = data;
		StatusCode = 200;
	}
}
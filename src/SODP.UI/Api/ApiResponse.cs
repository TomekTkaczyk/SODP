using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Api
{
	public record ApiResponse
	{
		public string Message { get; set; }

		public bool IsSuccess { get; set; }

		public ICollection<Error> Errors { get; set; }

	}

	public record ApiResponse<TValue> : ApiResponse
	{
		public TValue Value { get; set; }
	}
}

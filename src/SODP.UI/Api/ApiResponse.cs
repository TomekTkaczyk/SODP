using SODP.Shared.Response;
using System.Collections.Generic;
using System.Net;

namespace SODP.UI.Api
{
	public record ApiResponse
	{
		public HttpStatusCode HttpCode { get; init; }

		public string Message { get; init; }

		public bool IsSuccess { get; init; }

		public ICollection<Error> Errors { get; init; }

	}

	public record ApiResponse<TValue> : ApiResponse
	{
		public TValue Value { get; init; }
	}
}

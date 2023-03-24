using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SODP.Shared.Response
{
	public class Result
	{
		public string Message { get; set; } = "";
		public int StatusCode { get; set; } = StatusCodes.Status204NoContent;
		public IDictionary<string, string> ValidationErrors { get; set; } = new Dictionary<string, string>();


		public void SetError(string message)
		{
			SetError(message, StatusCodes.Status500InternalServerError);
		}

		public void SetError(string message, int statusCode)
		{
			Message += message;
			StatusCode = statusCode;
		}

		public static Result Success()
		{
			return new Result();
		}
	}

	public class Result<TResponse> : Result
	{
		public TResponse Data { get; set; }
		public void SetData(TResponse data)
		{
			Data = data;
			StatusCode = StatusCodes.Status200OK;
		}
	}
}

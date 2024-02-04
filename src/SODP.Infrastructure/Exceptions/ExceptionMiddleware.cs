using Microsoft.AspNetCore.Http;
using SODP.Domain.Exceptions;
using SODP.Shared.Response;
using System.Net;

namespace SODP.Infrastructure.Exceptions
{
	public sealed class ExceptionMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(ex, context);
			}
		}

		private async Task HandleExceptionAsync(Exception exception, HttpContext context)
		{
			var (statusCode, error) = exception switch
			{
				DomainException => (StatusCodes.Status400BadRequest, new Error(
					exception.GetType().Name.Replace("Exception", string.Empty), exception.Message)),
				_ => (StatusCodes.Status500InternalServerError, new Error("error", "Unknow server error"))
			};

			context.Response.StatusCode = statusCode;
			await context.Response.WriteAsJsonAsync(ApiResponse.Failure(
				error.Reason,HttpStatusCode.BadRequest,new List<Shared.Response.Error>()));
		}
	}

	public class Error
	{
		public string Code { get; init; }
		public string Reason { get; init; }
		public Error(string code, string reason)
		{
			Code = code;
			Reason = reason;
		}
	}
}

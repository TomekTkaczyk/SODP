using System.Net;

namespace SODP.Shared.Response;

public static class ErrorType
{
	public const string InternalServerError = "INTERNAL_SERVER_ERROR"; 
	
	public const string NotFound = "NOT_FOUND"; 
	
	public const string Confllict = "CONFLICT";
	
	public const string Unauthorizsed = "UNAUTHORISED"; 
	
	public static HttpStatusCode GetHttpStatusCode(string errorType)
	{
		return errorType switch
		{
			ErrorType.InternalServerError => HttpStatusCode.InternalServerError,
			ErrorType.NotFound => HttpStatusCode.NotFound,
			ErrorType.Confllict => HttpStatusCode.Conflict,
			ErrorType.Unauthorizsed => HttpStatusCode.Unauthorized,
			_ => HttpStatusCode.InternalServerError,
		};
	}
}

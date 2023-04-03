using System.Collections.Generic;
using System.Net;

namespace SODP.Shared.Response;

public sealed class Error : ValueObject
{
	public string Code { get; }
	public string Message { get; }
	public HttpStatusCode StatusCode { get;  }

	public Error(string code, string message, HttpStatusCode statusCode = HttpStatusCode.NoContent)
	{
		Code = code;
		Message = message;
		StatusCode = statusCode;
	}

	public static implicit operator string(Error error) => error?.Code ?? string.Empty;

	protected override IEnumerable<object> GetAtomicValues()
	{
		yield return Code;
		yield return Message;
	}

	public static Error None => new(string.Empty, string.Empty);
}

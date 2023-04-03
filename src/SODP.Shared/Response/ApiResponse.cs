using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;

namespace SODP.Shared.Response;

public record ApiResponse
{
	public string Message { get; set; }

	public bool IsSuccess { get; set; }
	
	public bool IsFailure;

	public HttpStatusCode StatusCode { get; }

	public ICollection<Error> Errors { get; } = new Collection<Error>();

	public ApiResponse(
		bool isSuccess, 
		ICollection<Error> errors, 
		HttpStatusCode statusCode = HttpStatusCode.NoContent) 
	{
		if (isSuccess && errors != null && errors.Count > 0)
		{
			throw new InvalidOperationException();
		}

		if (!isSuccess && (errors == null || errors.Count == 0))
		{
			throw new InvalidOperationException();
		}

		IsSuccess = isSuccess;
		IsFailure = !IsSuccess;
		Errors = errors;
		StatusCode = statusCode;
	}

	public static ApiResponse Success(HttpStatusCode statusCode = HttpStatusCode.OK) => new(true, new Collection<Error>(), statusCode);
	public static ApiResponse Failure(Error error, HttpStatusCode statusCode) => new(false, new Collection<Error>() { error }, statusCode);

	public ApiResponse AddError(Error error)
	{
		Errors.Add(error);
		
		return this;
	}

	public static ApiResponse<TValue> Success<TValue>(TValue value, HttpStatusCode statusCode = HttpStatusCode.OK) => new(value, true, new Collection<Error>(), statusCode);
	public static ApiResponse<TValue> Failure<TValue>(Error error, HttpStatusCode statusCode) => new(default!, false, new Collection<Error>() { error }, statusCode);

}

public record ApiResponse<TValue> : ApiResponse
{
	private readonly TValue _value;

	public TValue Value => IsSuccess
		? _value
		: throw new InvalidOperationException("The value of a failure result can not be accessed.");

	public ApiResponse(TValue value,  bool isSuccess, ICollection<Error> errors, HttpStatusCode statusCode) 
		: base(isSuccess, errors, statusCode) => _value = value;

	//public static implicit operator ApiResponse<TValue>(TValue value, HttpStatusCode statusCode) => Success(value, statusCode);


}

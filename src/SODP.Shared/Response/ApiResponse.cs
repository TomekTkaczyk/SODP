using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

namespace SODP.Shared.Response;

public record ApiResponse
{
	public string Message { get; }

	public bool IsSuccess { get; }
	
	public ICollection<Error> Errors { get; }

	internal protected ApiResponse(
		bool isSuccess, 
		string message, 
		ICollection<Error> errors) 
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
		Message = message;
		Errors = errors;
	}


	public static ApiResponse Success()
	=> new(true, string.Empty, new Collection<Error>());

	public static ApiResponse Failure(string message, Error error = null) 
		=> new(false, message, new Collection<Error>() { error }.Where(x => x != null).ToList());

	public static ApiResponse<TValue> Success<TValue>(TValue value)
		=> new(value, true, "", new Collection<Error>());

	public static ApiResponse<TValue> Failure<TValue>(string message)
	=> new(false, message, new Collection<Error>());

	public static ApiResponse<TValue> Failure<TValue>(string message, Error error = null)
	=> new(false, message, new Collection<Error>() { error }.Where(x => x != null).ToList());

	public ApiResponse AddError(Error error)
	{
		Errors.Add(error);

		return this;
	}
}

public record ApiResponse<TValue> : ApiResponse
{
	private readonly TValue _value;

	public TValue Value => IsSuccess
		? _value
		: throw new InvalidOperationException("The value of a failure result can not be accessed.");

	internal protected ApiResponse(TValue value, bool isSuccess)
		: base(isSuccess, "", new Collection<Error>()) => _value = value;

	internal protected ApiResponse(TValue value,  bool isSuccess, string message) 
		: base(isSuccess, message, new Collection<Error>()) => _value = value;

	internal protected ApiResponse(TValue value, bool isSuccess, string message, ICollection<Error> errors)
		: base(isSuccess, message, errors) => _value = value;

	internal protected ApiResponse(bool isSuccess, string message, ICollection<Error> errors)
		: base(isSuccess, message, errors) { }
}

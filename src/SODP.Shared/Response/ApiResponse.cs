using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SODP.Shared.Response;

public record ApiResponse
{
	public bool IsSuccess { get; set; }
	public int StatusCode { get; set; }
	public string Message { get; set; }
	
	public bool IsFailure;

	public ICollection<Error> Errors { get; } = new Collection<Error>();

	public ApiResponse(bool isSuccess, ICollection<Error> errors) 
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
	}

	public static ApiResponse Success() => new(true, new Collection<Error>());
	public static ApiResponse Failure(Error error) => new(false, new Collection<Error>() { error });

	public ApiResponse AddError(Error error)
	{
		Errors.Add(error);
		
		return this;
	}

	public static ApiResponse<TValue> Success<TValue>(TValue value) => new(value, true, new Collection<Error>());
	public static ApiResponse<TValue> Failure<TValue>(Error error) => new(default!, false, new Collection<Error>() { error });

}

public record ApiResponse<TValue> : ApiResponse
{
	private readonly TValue _value;

	public TValue Value => IsSuccess
		? _value
		: throw new InvalidOperationException("The value of a failure result can not be accessed.");

	public ApiResponse(TValue value,  bool isSuccess, ICollection<Error> errors) 
		: base(isSuccess, errors) => _value = value;

	public static implicit operator ApiResponse<TValue>(TValue value) => Success(value);


}

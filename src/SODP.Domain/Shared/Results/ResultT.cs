﻿using SODP.Shared.Response;
using System;

namespace SODP.Domain.Shared.Results;

public class Result<TValue> : Result
{
    private readonly TValue _value;

    public Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error) => _value = value;

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue value) => Create(value);

    private static Result<TValue> Create(TValue value)
    {
        throw new NotImplementedException();
    }
}
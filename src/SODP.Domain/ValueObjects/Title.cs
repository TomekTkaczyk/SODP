﻿namespace SODP.Domain.ValueObjects;

public record Title
{
	public string Value { get; }

    public Title(string title)
    {
        Value = title;
    }

	public static implicit operator string(Title title) => title?.Value;

	public static implicit operator Title(string title) => new(title);

}
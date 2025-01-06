﻿using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Acquaintances.Bot.Domain.ValueObjects.Profile;

public class Name : ValueObject
{
    public const int NameLength = 20;

    public string Value { get; private set; }

	[JsonConstructor]
	protected Name(string value)
    {
        Value = value;
    }

    public static Result<Name> Create(string? value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            return Result.Failure<Name>("Имя не может быть пустым");

        if (value.Trim().Length > NameLength)
            return Result.Failure<Name>($"Имя не может быть больше {NameLength} символов");

        return new Name(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

	public override string ToString()
	{
        return Value;
	}
}

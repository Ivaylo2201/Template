using System.Diagnostics.CodeAnalysis;

namespace Template.Application.Common;

public enum ErrorType
{
    Invalid,
    NotFound,
    Forbidden
}

public class Result<TValue>
{
    public readonly ErrorType? Error;
    public readonly TValue? Value;

    private Result(TValue? value, ErrorType? error)
    {
        Value = value;
        Error = error;
    }

    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess => Error is null && Value is not null;

    public static Result<TValue> Success(TValue value) => new(value, null);
    public static Result<TValue> Failure(ErrorType error) => new(default, error);
}
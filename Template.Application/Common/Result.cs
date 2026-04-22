using System.Diagnostics.CodeAnalysis;

namespace Template.Application.Common;

public class Result<TValue>
{
    public readonly Enum? ErrorCode;
    public readonly TValue? Value;

    private Result(TValue? value, Enum? errorCode)
    {
        Value = value;
        ErrorCode = errorCode;
    }

    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(ErrorCode))]
    public bool IsSuccess => ErrorCode is null;

    public static Result<TValue> Success(TValue value) => new(value, null);
    public static Result<TValue> Failure(Enum error) => new(default, error);
}
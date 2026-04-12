using System.Diagnostics.CodeAnalysis;

namespace Template.Application.Common;

public enum ResultStatus
{
    Ok = 0,
    Created,
    Invalid,
    NotFound,
    Forbidden
}

public class Result<TValue>
{
    public readonly ResultStatus Status;
    public readonly TValue? Value;

    private Result(ResultStatus status, TValue? value, bool isSuccess)
    {
        Status = status;
        Value = value;
        IsSuccess = isSuccess;
    }

    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess => field && Value is not null;

    public static Result<TValue> Failure(ResultStatus status) => new(status, default, false);
    public static Result<TValue> Success(ResultStatus status, TValue value) => new(status, value, true);
}
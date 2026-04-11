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

    private Result(ResultStatus status, TValue? value)
    {
        Status = status;
        Value = value;
    }
    
    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess => Value is not null && !EqualityComparer<TValue>.Default.Equals(Value, default);

    public static implicit operator Result<TValue>((ResultStatus Status, TValue Value) tuple) 
        => new(tuple.Status, tuple.Value);
    
    public static implicit operator Result<TValue>(ResultStatus status) 
        => new(status, default);
}
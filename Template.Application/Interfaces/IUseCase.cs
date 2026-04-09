using Template.Application.Common;

namespace Template.Application.Interfaces;

public interface IUseCase<in TRequest, TResponse>
{
    Task<Result<TResponse>> ExecuteAsync(TRequest request, CancellationToken ct);
}
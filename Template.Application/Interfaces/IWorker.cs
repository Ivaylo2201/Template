using Template.Application.Common;

namespace Template.Application.Interfaces;

public interface IWorker<in TRequest, TResponse>
{
    Task<Result<TResponse>> ProcessAsync(TRequest request, CancellationToken ct);
}
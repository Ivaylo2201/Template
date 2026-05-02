using Microsoft.Extensions.Logging;
using Template.Application.Common;
using Template.Application.Interfaces;

namespace Template.Application.UseCases.ComplexUseCase;

public class ComplexUseCaseWorker(
    ILogger<ComplexUseCaseWorker> logger,
    Pipeline<ComplexUseCaseContext> pipeline) : IWorker<ComplexUseCaseRequest, ComplexUseCaseResponse>
{
    public async Task<Result<ComplexUseCaseResponse>> ExecuteAsync(ComplexUseCaseRequest request, CancellationToken ct)
    {
        var ctx = new ComplexUseCaseContext(request);
        await pipeline.ExecuteAsync(ctx, ct);
        
        return Result<ComplexUseCaseResponse>.Success(new ComplexUseCaseResponse());
    }
}
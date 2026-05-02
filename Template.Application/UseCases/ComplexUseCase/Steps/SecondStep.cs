using Microsoft.Extensions.Logging;
using Template.Application.Interfaces;

namespace Template.Application.UseCases.ComplexUseCase.Steps;

public class SecondStep(ILogger<ComplexUseCaseWorker> logger) : IPipelineStep<ComplexUseCaseContext>
{
    public int Order => 2;
    
    public async Task<bool> ExecuteAsync(ComplexUseCaseContext ctx, CancellationToken ct)
    {
        logger.LogInformation("Second step");
        return true;
    }
}
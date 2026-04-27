using Microsoft.Extensions.Logging;
using Template.Application.Interfaces;

namespace Template.Application.UseCases.ComplexUseCase.Steps;

public class FirstStep(ILogger<ComplexUseCaseWorker> logger) : IPipelineStep<ComplexUseCaseContext>
{
    public int Order => 1;
    
    public async Task<bool> ExecuteAsync(ComplexUseCaseContext ctx, CancellationToken ct)
    {
        logger.LogInformation("First step");
        return false;
    }
}
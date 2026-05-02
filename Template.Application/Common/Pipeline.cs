using Template.Application.Interfaces;

namespace Template.Application.Common;

public class Pipeline<TContext>(IEnumerable<IPipelineStep<TContext>> steps)
{
    private readonly IEnumerable<IPipelineStep<TContext>> _steps = steps.OrderBy(s => s.Order);
    
    public async Task ExecuteAsync(TContext ctx, CancellationToken ct)
    {
        foreach (var step in _steps)
        {
            if (!(await step.ExecuteAsync(ctx, ct)))
                break;
        }
    }
}

namespace Template.Application.Interfaces;

public interface IPipelineStep<in TContext>
{
    public int Order { get; }
    Task<bool> ExecuteAsync(TContext ctx, CancellationToken ct);
}

namespace Template.Application.UseCases.ComplexUseCase;

public record ComplexUseCaseContext(ComplexUseCaseRequest Request)
{
    public DateTime StartTime { get; init; } = DateTime.UtcNow;
}
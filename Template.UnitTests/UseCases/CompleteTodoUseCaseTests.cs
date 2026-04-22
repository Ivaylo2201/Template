using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Template.Application.Common;
using Template.Application.Interfaces;
using Template.Application.UseCases.CompleteTodo;
using Template.Domain.Entities;

namespace Template.UnitTests.UseCases;

public class CompleteTodoUseCaseTests
{
    private readonly Mock<ILogger<CompleteTodoUseCase>> _logger = new();
    private readonly Mock<IAppDbContext> _dbContext = new();
    private readonly CompleteTodoRequest _request = new(1);

    [Fact]
    public async Task CompleteTodo_ShouldMarkTodoAsCompleted_WhenTodoExists()
    {
        _dbContext
            .Setup(x => x.Todos)
            .ReturnsDbSet([new Todo { Id = 1 }]);

        var useCase = new CompleteTodoUseCase(_logger.Object, _dbContext.Object);
        var result = await useCase.ExecuteAsync(_request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.ErrorCode.Should().BeNull();
    }
    
    [Fact]
    public async Task CompleteTodo_ShouldReturnAlreadyCompleted_WhenTodoIsAlreadyCompleted()
    {
        _dbContext
            .Setup(x => x.Todos)
            .ReturnsDbSet([new Todo { Id = 1, IsCompleted = true }]);

        var useCase = new CompleteTodoUseCase(_logger.Object, _dbContext.Object);
        var result = await useCase.ExecuteAsync(_request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.ErrorCode.Should().Be(CompleteTodoError.AlreadyCompleted);
    }
    
    [Fact]
    public async Task CompleteTodo_ShouldReturnNotFound_WhenTodoDoesNotExist()
    {
        _dbContext
            .Setup(x => x.Todos)
            .ReturnsDbSet([]);
        
        var useCase = new CompleteTodoUseCase(_logger.Object, _dbContext.Object);
        var result = await useCase.ExecuteAsync(_request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.ErrorCode.Should().Be(Error.NotFound);
    }
}
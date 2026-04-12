using Microsoft.Extensions.Logging;

namespace Template.Application.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Warning, Message = "Validation failed - {@Errors}")]
    public static partial void ValidationFailed(this ILogger logger, IEnumerable<string> errors);
    
    [LoggerMessage(EventId = 1, Level = LogLevel.Error, Message = "Todo with Id = {Id} not found.")]
    public static partial void TodoNotFound(this ILogger logger, int id);
}
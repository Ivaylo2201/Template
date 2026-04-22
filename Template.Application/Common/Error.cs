using System.ComponentModel;

namespace Template.Application.Common;

public enum Error
{
    [Description("Request validation failed.")]
    Invalid,
    [Description("Resource not found.")]
    NotFound,
    [Description("Access forbidden.")]
    Forbidden
}

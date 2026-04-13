using System.Reflection;
using System.Text.Json.Serialization;

namespace Template.Application.Extensions;

public static class EnumExtensions
{
    public static string GetEnumMemberName(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString())[0];
        var attr = member.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();

        return attr?.Name ?? value.ToString();
    }
}
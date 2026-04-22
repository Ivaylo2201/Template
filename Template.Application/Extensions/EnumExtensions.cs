using System.ComponentModel;
using System.Reflection;

namespace Template.Application.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString())[0];
        var attr = member.GetCustomAttribute<DescriptionAttribute>();

        return attr?.Description ?? value.ToString();
    }
}
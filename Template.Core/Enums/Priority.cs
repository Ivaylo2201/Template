using System.Text.Json.Serialization;

namespace Template.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Priority
{
    [JsonStringEnumMemberName("Low")]
    Low,

    [JsonStringEnumMemberName("Medium")]
    Medium,

    [JsonStringEnumMemberName("High")]
    High,

    [JsonStringEnumMemberName("Critical")]
    Critical
}
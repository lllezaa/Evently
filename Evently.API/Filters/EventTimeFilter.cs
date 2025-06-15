using System.Text.Json.Serialization;

namespace Evently.API.Filters;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventTimeFilter
{
    All,
    Upcoming,
    Past
}
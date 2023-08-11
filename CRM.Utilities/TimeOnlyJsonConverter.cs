using System.Text.Json.Serialization;
using System.Text.Json;

namespace CRM.Utilities;
public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var timeString = reader.GetString();
        if(timeString == null || !DateTime.TryParse(timeString, out var timeOnly))
        {
            throw new RequestException("Time was in an incorrect format", System.Net.HttpStatusCode.BadRequest);
        }
        return TimeOnly.FromDateTime(timeOnly);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString("o")[..5];
        writer.WriteStringValue(isoDate);
    }
}
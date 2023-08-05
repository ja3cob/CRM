using System.Text.Json.Serialization;
using System.Text.Json;

namespace CRM.Services.Util;
public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.FromDateTime(reader.GetDateTime());
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString();
        writer.WriteStringValue(isoDate);
    }
}
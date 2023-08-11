using System.Text.Json.Serialization;
using System.Text.Json;

namespace CRM.Utilities;
public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if(reader.GetString() == null)
        {

        }
        return TimeOnly.FromDateTime(DateTime.Parse(reader.GetString()));
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString("o")[..5];
        writer.WriteStringValue(isoDate);
    }
}
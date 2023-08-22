using System.Text.Json.Serialization;
using System.Text.Json;

namespace CRM.Utilities.Converters;
public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!reader.TryGetDateTime(out var dateTime))
        {
            throw new ApiException("Date was in an incorrect format", System.Net.HttpStatusCode.BadRequest);
        }
        return DateOnly.FromDateTime(dateTime);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        var isoDate = value.ToString("O");
        writer.WriteStringValue(isoDate);
    }
}
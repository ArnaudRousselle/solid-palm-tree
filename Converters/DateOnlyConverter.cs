using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyPersonalAccounting.Converters;

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private const string _serializationFormat = "yyyyMMdd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.ParseExact(value!, _serializationFormat);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(_serializationFormat));
}
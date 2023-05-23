using System.Text.Json;
using System.Text.Json.Serialization;
using MyPersonalAccounting.ActorSystem;

namespace MyPersonalAccounting.Converters;

public class ItemIdentityConverter<T> : JsonConverter<T> where T : ItemIdentity
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (value != null)
            return Activator.CreateInstance(typeof(T), new object[] { value }) as T;
        else
            return Activator.CreateInstance<T>();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Guid.ToString());
}
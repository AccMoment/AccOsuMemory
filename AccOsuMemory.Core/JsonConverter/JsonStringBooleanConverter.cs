using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Int32;

namespace AccOsuMemory.Core.JsonConverter;

public class JsonStringBooleanConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetInt32(out var value))
        {
            return value == 1;
        }
        TryParse(reader.GetString(), out var result);
        return result == 1;
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value ? "1" : "0");
    }
}
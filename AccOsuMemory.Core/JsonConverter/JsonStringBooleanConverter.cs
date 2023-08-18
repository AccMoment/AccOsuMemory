using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Int32;

namespace AccOsuMemory.Core.JsonConverter;

public class JsonStringBooleanConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        TryParse(reader.GetString(), out int result);
        return result == 1;
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value ? "1" : "0");
    }
}
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AccOsuMemory.Core.JsonConverter;

public class JsonTimestampDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var fromUnixTimeSeconds = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64());
        return fromUnixTimeSeconds.DateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
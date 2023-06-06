using System.Text.Json;
using System.Text.Json.Serialization;

namespace AccOsuMemory.Core.OsuApi.Utils;

public class JsonStringDateTimeConverter :JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)=>
    DateTime.Parse(reader.GetString()!);


    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)=>
        writer.WriteStringValue(value.ToString("yyyy-M-d hh:mm:ss"));
    
}
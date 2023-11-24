using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace CalqFramework.Serialization.Json {
    public class JsonSerializer : ISerializer
    {
        private JsonSerializerOptions JsonSerializerOptions { get; } = new();

        public JsonSerializer() { }

        public JsonSerializer(JsonSerializerOptions jsonSerializerOptions) {
            JsonSerializerOptions = jsonSerializerOptions;
        }

        public ReadOnlySpan<byte> Serialize<T>(T obj)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj, JsonSerializerOptions);
        }

        public void Populate<T>(ReadOnlySpan<byte> data, T obj)
        {
            JsonConvert.PopulateObject(Encoding.UTF8.GetString(data), obj);
        }

    }
}

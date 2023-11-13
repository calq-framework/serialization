using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace CalqFramework.Serialization.Json {
    public class JsonSerializer : ICalqSerializer {

        // TODO pass this via constructor
        private readonly JsonSerializerOptions serializerOptions = new() {
            IncludeFields = true
        };

        public ReadOnlySpan<byte> Serialize<T>(T obj) {
            var json = System.Text.Json.JsonSerializer.Serialize(obj, serializerOptions);
            return Encoding.UTF8.GetBytes(json);
        }

        public void Populate<T>(ReadOnlySpan<byte> data, T obj) {
            JsonConvert.PopulateObject(Encoding.UTF8.GetString(data), obj);
        }

    }
}

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenTest {
    internal static class Program {
        static void Main(string[] args) {

#if DEBUG
            new JsonDeserializeCompare().DebugTest();
#else
            var summary = BenchmarkRunner.Run<JsonDeserializeCompare>();
#endif

        }
    }

    public class JsonDeserializeCompare {
        private string jsonString;
        private byte[] jsonBytes;
        private MetadataJsonClassContext metadataJsonClassContext;
        private DefaultJsonClassContext defaultJsonClassContext;
        private SomeData jsonObj;
        private CalqFramework.Serialization.Json.JsonSerializer newtonsoftSerializer = new();
        private CalqFramework.Serialization.Json.SimpleJsonSerializer calqSerializer = new();

        public JsonDeserializeCompare() {
            //_jsonStringToDeserialize = "{\"Name\":\"John\",\"Surname\":\"Smith\",\"Age\":25}";
            jsonString = "{\"Name\":\"John\",\"Surname\":\"Smith\",\"Age\":\"25\"}";
            jsonBytes = Encoding.UTF8.GetBytes(jsonString);

            metadataJsonClassContext = new(new JsonSerializerOptions() { TypeInfoResolver = MetadataJsonClassContext.Default });
            defaultJsonClassContext = new(new JsonSerializerOptions() { TypeInfoResolver = DefaultJsonClassContext.Default });
            jsonObj = new();
        }

        public void DebugTest() {
            jsonObj = new();
            calqSerializer.Populate(jsonBytes, jsonObj);
        }

        [Benchmark(Baseline = true)]
        public SomeData DeserializeWithSystemTextJson() {
            return System.Text.Json.JsonSerializer.Deserialize<SomeData>(jsonBytes);
        }

        [Benchmark]
        public SomeData DeserializeWithMetadataJsonClassContext() {
            return System.Text.Json.JsonSerializer.Deserialize(jsonBytes, metadataJsonClassContext.SomeData);
        }

        [Benchmark]
        public SomeData DeserializeWithDefaultJsonClassContext() {
            return System.Text.Json.JsonSerializer.Deserialize(jsonBytes, defaultJsonClassContext.SomeData);
        }

        [Benchmark]
        public SomeData DeserializeWithNewtonsoftJson() {
            return JsonConvert.DeserializeObject<SomeData>(jsonString);
        }

        [Benchmark]
        public SomeData DeserializeWithCalqSerializer() {
            var jsonClass = new SomeData();
            calqSerializer.Populate(jsonBytes, jsonObj);
            return jsonClass;
        }
    }

    public class SomeData {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Age { get; set; }
    }

    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified, GenerationMode = JsonSourceGenerationMode.Metadata)]
    [JsonSerializable(typeof(SomeData))]
    internal partial class MetadataJsonClassContext : JsonSerializerContext {
    }

    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified, GenerationMode = JsonSourceGenerationMode.Default)]
    [JsonSerializable(typeof(SomeData))]
    internal partial class DefaultJsonClassContext : JsonSerializerContext {
    }

    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified, GenerationMode = JsonSourceGenerationMode.Serialization)]
    [JsonSerializable(typeof(SomeData))]
    internal partial class SerializationJsonClassContext : JsonSerializerContext {
    }
}

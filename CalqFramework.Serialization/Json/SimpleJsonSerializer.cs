using System.Text.Json;
using CalqFramework.Serialization.DataMemberAccess;

namespace CalqFramework.Serialization.Json {
    public partial class SimpleJsonSerializer : ISerializer
    {
        private DataMemberAccessor DataMemberAccessor { get; } = new();

        private JsonSerializerOptions JsonSerializerOptions { get; } = new();

        public SimpleJsonSerializer() { }

        public SimpleJsonSerializer(DataMemberAccessor dataMemberAccessor, JsonSerializerOptions jsonSerializerOptions) {
            DataMemberAccessor = dataMemberAccessor;
            JsonSerializerOptions = jsonSerializerOptions;
        }
    }
}

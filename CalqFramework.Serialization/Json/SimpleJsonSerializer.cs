using System.Text.Json;
using CalqFramework.Serialization.DataMemberAccess;

namespace CalqFramework.Serialization.Json {
    public partial class SimpleJsonSerializer : ISerializer
    {
        private IDataMemberAccessor DataMemberAccessor { get; } = DataMemberAccessorFactory.DefaultDataMemberAccessor;

        private JsonSerializerOptions JsonSerializerOptions { get; } = new();

        public SimpleJsonSerializer() { }

        public SimpleJsonSerializer(IDataMemberAccessor dataMemberAccessor, JsonSerializerOptions jsonSerializerOptions) {
            DataMemberAccessor = dataMemberAccessor;
            JsonSerializerOptions = jsonSerializerOptions;
        }
    }
}

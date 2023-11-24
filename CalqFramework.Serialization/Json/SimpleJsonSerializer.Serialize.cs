namespace CalqFramework.Serialization.Json {
    public partial class SimpleJsonSerializer : ISerializer
    {
        public ReadOnlySpan<byte> Serialize<T>(T obj) {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj, JsonSerializerOptions);
        }
    }
}

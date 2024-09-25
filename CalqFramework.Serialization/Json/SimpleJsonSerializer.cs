using System.Text.Json;
using CalqFramework.Serialization.DataAccess.DataMemberAccess;

namespace CalqFramework.Serialization.Json
{
    public partial class SimpleJsonSerializer : ISerializer
    {

        public JsonSerializerOptions JsonSerializerOptions { get; init; }
        public DataMemberAccessorOptions DataMemberAccessorOptions { get; init; }

        private DataMemberAccessorFactory DataMemberAccessorFactory { get; init; }

        public SimpleJsonSerializer() {
            JsonSerializerOptions ??= new();
            DataMemberAccessorOptions ??= new();
            DataMemberAccessorFactory ??= new DataMemberAccessorFactory(DataMemberAccessorOptions);
        }
    }
}

using System.Text.Json;
using CalqFramework.Serialization.DataAccess.ClassMember;

namespace CalqFramework.Serialization.Json
{
    public partial class SimpleJsonSerializer : ISerializer
    {

        public JsonSerializerOptions JsonSerializerOptions { get; init; }
        public ClassDataMemberStoreFactoryOptions ClassDataMemberStoreOptions { get; init; }

        private ClassDataMemberStoreFactory ClassDataMemberStoreFactory { get; init; }

        public SimpleJsonSerializer() {
            JsonSerializerOptions ??= new();
            ClassDataMemberStoreOptions ??= new();
            ClassDataMemberStoreFactory ??= new ClassDataMemberStoreFactory(ClassDataMemberStoreOptions);
        }
    }
}

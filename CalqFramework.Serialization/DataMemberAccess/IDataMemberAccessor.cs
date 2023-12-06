using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    public interface IDataMemberAccessor {
        public IDictionary<string, MemberInfo> GetDataMembersByKeys(Type type);
        public MemberInfo? GetDataMember(Type type, string dataMemberKey);

        public Type GetDataMemberType(Type type, string dataMemberKey);

        public object? GetDataMemberValue(object obj, string dataMemberKey);

        public object GetOrInitializeDataMemberValue(object obj, string dataMemberKey);

        public void SetDataMemberValue(object obj, string dataMemberKey, object? value);
    }
}
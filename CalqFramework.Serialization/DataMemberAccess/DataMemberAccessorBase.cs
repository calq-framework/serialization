using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    public abstract class DataMemberAccessorBase : IDataMemberAccessor {

        public BindingFlags BindingAttr { get; }

        public DataMemberAccessorBase(BindingFlags bindingAttr) {
            BindingAttr = bindingAttr;
        }

        public abstract MemberInfo[] GetDataMembers(Type type);

        public abstract MemberInfo? GetDataMember(Type type, string dataMemberKey);

        public MemberInfo GetDataMemberOrThrow(Type type, string dataMemberKey) {
            return GetDataMember(type, dataMemberKey) ?? throw new MissingMemberException();
        }

        public abstract Type GetDataMemberType(Type type, string dataMemberKey);
        public abstract object? GetDataMemberValue(object obj, string dataMemberKey);
        public abstract object GetOrInitializeDataMemberValue(object obj, string dataMemberKey);
        public abstract void SetDataMemberValue(object obj, string dataMemberKey, object? value);
    }
}
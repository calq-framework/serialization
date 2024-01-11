using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    public abstract class DataMemberAccessorBase : IDataAccessor {
        public object Obj { get; }
        public Type Type { get; }
        public BindingFlags BindingAttr { get; }

        public DataMemberAccessorBase(object obj, BindingFlags bindingAttr) {
            Obj = obj;
            BindingAttr = bindingAttr;
            Type = obj.GetType();
        }

        public abstract IDictionary<string, MemberInfo> GetDataMembersByKeys();

        public abstract MemberInfo? GetDataMember(string dataMemberKey);

        public MemberInfo GetDataMemberOrThrow(string dataMemberKey) {
            return GetDataMember(dataMemberKey) ?? throw new MissingMemberException();
        }

        public abstract Type GetType(string dataMemberKey);
        public abstract object? GetValue(string dataMemberKey);
        public abstract object GetOrInitializeValue(string dataMemberKey);
        public abstract void SetValue(string dataMemberKey, object? value);
        public bool HasKey(string dataMemberKey) {
            var dataMember = GetDataMember(dataMemberKey);

            return dataMember == null ? false : true;
        }
    }
}

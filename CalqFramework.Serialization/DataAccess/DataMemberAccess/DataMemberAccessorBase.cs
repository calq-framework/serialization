using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    public abstract class DataMemberAccessorBase : IDataMemberAccessor {
        public object Obj { get; }
        public Type Type { get; }
        public BindingFlags BindingAttr { get; }

        public DataMemberAccessorBase(object obj, BindingFlags bindingAttr) {
            Obj = obj;
            BindingAttr = bindingAttr;
            Type = obj.GetType();
        }

        public abstract IDictionary<string, MemberInfo> GetDataMembersByKeys();

        public abstract MemberInfo? GetDataMember(string key);

        public MemberInfo GetDataMemberOrThrow(string key) {
            return GetDataMember(key) ?? throw new MissingMemberException();
        }

        public abstract Type GetType(string key);
        public abstract object? GetValue(string key);
        public abstract object GetOrInitializeValue(string key);
        public abstract void SetValue(string key, object? value);
        public bool HasKey(string key) {
            var dataMember = GetDataMember(key);

            return dataMember == null ? false : true;
        }
    }
}

using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    public abstract class DataMemberAccessorBase : DataAccessorBase, IDataMemberAccessor {
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

        public override bool HasKey(string key) {
            var dataMember = GetDataMember(key);

            return dataMember == null ? false : true;
        }
    }
}

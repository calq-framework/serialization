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

        public bool TryGetDataMember(string key, out MemberInfo result) {
            var dataMember = GetDataMemberCore(key);
            if (dataMember != null) {
                result = dataMember;
                return true;
            }
            result = null!;
            return false;
        }

        public MemberInfo GetDataMember(string key) {
            if(TryGetDataMember(key, out var dataMember)) {
                return dataMember;
            } else {
                throw new MissingMemberException();
            }
        }

        protected abstract MemberInfo? GetDataMemberCore(string key);

        public override bool HasKey(string key) {
            var dataMember = GetDataMemberCore(key);

            return dataMember == null ? false : true;
        }
    }
}

using Newtonsoft.Json.Linq;
using System.Collections;
using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    public abstract class ClassMemberResolverBase<TKey, TValue> : IClassMemberResolver<TKey, MemberInfo> {
        public object Obj { get; }
        public Type Type { get; }
        public BindingFlags BindingAttr { get; }

        public ClassMemberResolverBase(object obj, BindingFlags bindingAttr) {
            Obj = obj;
            BindingAttr = bindingAttr;
            Type = obj.GetType();
        }

        public abstract bool HasDataMember(MemberInfo memberInfo);

        public abstract IDictionary<TKey, MemberInfo> GetDataMembersByKeys();

        public bool TryGetMediaryKey(TKey key, out MemberInfo result) {
            var dataMember = GetDataMemberCore(key);
            if (dataMember != null) {
                result = dataMember;
                return true;
            }
            result = null!;
            return false;
        }

        public MemberInfo GetMediaryKey(TKey key) {
            if(TryGetMediaryKey(key, out var dataMember)) {
                return dataMember;
            } else {
                throw new MissingMemberException();
            }
        }

        public bool Contains(TKey key) {
            var dataMember = GetDataMemberCore(key);

            return dataMember == null ? false : true;
        }

        protected abstract MemberInfo? GetDataMemberCore(TKey key);
        public abstract string DataMemberToString(MemberInfo memberInfo);
    }
}

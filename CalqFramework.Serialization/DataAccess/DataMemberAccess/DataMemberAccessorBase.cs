using System.Collections;
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

        public abstract bool HasDataMember(MemberInfo memberInfo);

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

        public abstract string DataMemberToString(MemberInfo memberInfo);

        protected abstract MemberInfo? GetDataMemberCore(string key);

        public override bool HasKey(string key) {
            var dataMember = GetDataMemberCore(key);

            return dataMember == null ? false : true;
        }

        public bool Contains(MemberInfo member) {
            return HasKey(member.Name);
        }
        public Type GetType(MemberInfo member) {
            return GetType(member.Name);
        }
        public object? GetValue(MemberInfo member) {
            return GetValue(member.Name);
        }
        public object GetOrInitializeValue(MemberInfo member) {
            return GetOrInitializeValue(member.Name);
        }
        public void SetValue(MemberInfo member, object? value) {
            SetValue(member.Name, value);
        }
        public virtual bool SetOrAddValue(MemberInfo member, object? value) {
            var obj = GetValue(member);
            if (obj is not ICollection collectionObj) {
                SetValue(member, value);
                return false;
            } else {
                AddValue(collectionObj, value);
                return true;
            }
        }
    }
}

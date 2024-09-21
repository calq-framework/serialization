using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    sealed public class PropertyAccessor : PropertyAccessorBase
    {
        public PropertyAccessor(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr)
        {
        }

        public override string DataMemberToString(MemberInfo memberInfo) {
            return memberInfo.Name;
        }

        public override IDictionary<string, MemberInfo> GetDataMembersByKeys()
        {
            return Type.GetProperties(BindingAttr).ToDictionary(x => x.Name, x => (MemberInfo)x);
        }

        public override bool HasDataMember(MemberInfo memberInfo) {
            return memberInfo is PropertyInfo;
        }

        protected override MemberInfo? GetDataMemberCore(string key)
        {
            return Type.GetProperty(key, BindingAttr);
        }
    }
}
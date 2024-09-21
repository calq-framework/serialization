using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    sealed public class FieldAccessor : FieldAccessorBase
    {
        public FieldAccessor(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr)
        {
        }

        public override string DataMemberToString(MemberInfo memberInfo) {
            return memberInfo.Name;
        }

        public override IDictionary<string, MemberInfo> GetDataMembersByKeys()
        {
            return Type.GetFields(BindingAttr).ToDictionary(x => x.Name, x => (MemberInfo)x);
        }

        public override bool HasDataMember(MemberInfo memberInfo) {
            return memberInfo is FieldInfo;
        }

        protected override MemberInfo? GetDataMemberCore(string key) {
            return Type.GetField(key, BindingAttr);
        }
    }
}
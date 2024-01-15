using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    sealed public class PropertyAccessor : PropertyAccessorBase
    {
        public PropertyAccessor(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr)
        {
        }

        public override IDictionary<string, MemberInfo> GetDataMembersByKeys()
        {
            return Type.GetProperties(BindingAttr).ToDictionary(x => x.Name, x => (MemberInfo)x);
        }

        public override MemberInfo? GetDataMember(string key)
        {
            return Type.GetProperty(key, BindingAttr);
        }
    }
}
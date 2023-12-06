using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    sealed public class PropertyAccessor : PropertyAccessorBase {
        public PropertyAccessor(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override IDictionary<string, MemberInfo> GetDataMembersByKeys(Type type) {
            return type.GetProperties(BindingAttr).ToDictionary(x => x.Name, x => (MemberInfo)x);
        }

        public override MemberInfo? GetDataMember(Type type, string dataMemberKey) {
            return type.GetProperty(dataMemberKey, BindingAttr);
        }
    }
}
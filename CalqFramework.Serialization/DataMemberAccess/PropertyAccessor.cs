using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    sealed public class PropertyAccessor : PropertyAccessorBase {
        public PropertyAccessor(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override MemberInfo[] GetDataMembers(Type type) {
            return type.GetProperties(BindingAttr);
        }

        public override MemberInfo? GetDataMember(Type type, string dataMemberKey) {
            return type.GetProperty(dataMemberKey, BindingAttr);
        }
    }
}
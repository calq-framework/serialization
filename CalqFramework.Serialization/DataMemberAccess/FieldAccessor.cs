using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    sealed public class FieldAccessor : FieldAccessorBase {
        public FieldAccessor(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override MemberInfo[] GetDataMembers(Type type) {
            return type.GetFields(BindingAttr);
        }

        public override MemberInfo? GetDataMember(Type type, string dataMemberKey) {
            return type.GetField(dataMemberKey, BindingAttr);
        }
    }
}
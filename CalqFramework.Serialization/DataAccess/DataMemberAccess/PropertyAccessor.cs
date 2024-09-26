using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    sealed public class PropertyAccessor : PropertyAccessorBase<string> {
        public PropertyAccessor(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        protected override MemberInfo? GetClassMember(string key) {
            return ParentType.GetProperty(key, BindingAttr);
        }
    }
}
using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    sealed public class PropertyAccessor : PropertyAccessorBase<string> {
        public PropertyAccessor(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        protected override MemberInfo? GetDataMemberCore(string key) {
            return Type.GetProperty(key, BindingAttr);
        }
    }
}
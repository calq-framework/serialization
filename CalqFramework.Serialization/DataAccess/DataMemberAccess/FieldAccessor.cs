using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    sealed public class FieldAccessor : FieldAccessorBase<string> {
        public FieldAccessor(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        protected override MemberInfo? GetClassMember(string key) {
            return ParentType.GetField(key, BindingAttr);
        }
    }
}
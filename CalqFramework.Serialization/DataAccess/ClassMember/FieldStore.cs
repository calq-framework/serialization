using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.ClassMember
{
    sealed public class FieldStore : FieldStoreBase<string> {
        public FieldStore(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        protected override MemberInfo? GetClassMember(string key) {
            return ParentType.GetField(key, BindingAttr);
        }
    }
}
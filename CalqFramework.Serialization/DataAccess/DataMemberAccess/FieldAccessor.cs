using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    sealed public class FieldAccessor : FieldAccessorBase<string> {
        public FieldAccessor(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        protected override MemberInfo? GetDataMemberCore(string key) {
            return Type.GetField(key, BindingAttr);
        }
    }
}
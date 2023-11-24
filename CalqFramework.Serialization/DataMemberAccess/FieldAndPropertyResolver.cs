using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    internal class FieldAndPropertyResolver : DataMemberResolverBase {
        public FieldAndPropertyResolver(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override MemberInfo GetDataMember(Type type, string dataMemberKey) {
            return (MemberInfo?)type.GetField(dataMemberKey, BindingAttr) ?? (MemberInfo?)type.GetProperty(dataMemberKey) ?? throw new MissingMemberException();
        }
    }
}
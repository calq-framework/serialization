using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    internal class FieldResolver : DataMemberResolverBase {
        public FieldResolver(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override MemberInfo GetDataMember(Type type, string dataMemberKey) {
            return (MemberInfo?)type.GetField(dataMemberKey, BindingAttr) ?? throw new MissingMemberException();
        }
    }
}
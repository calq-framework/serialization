using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    internal class PropertyResolver : DataMemberResolverBase {
        public PropertyResolver(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override MemberInfo GetDataMember(Type type, string dataMemberKey) {
            return (MemberInfo?)type.GetProperty(dataMemberKey, BindingAttr) ?? throw new MissingMemberException();
        }
    }
}
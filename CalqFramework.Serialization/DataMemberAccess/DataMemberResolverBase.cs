using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    public abstract class DataMemberResolverBase : IDataMemberResolver {

        public BindingFlags BindingAttr { get; }

        public DataMemberResolverBase(BindingFlags bindingAttr) {
            BindingAttr = bindingAttr;
        }

        public abstract MemberInfo GetDataMember(Type type, string dataMemberKey);
    }
}
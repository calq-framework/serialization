using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    public interface IDataMemberResolver {
        public MemberInfo GetDataMember(Type type, string dataMemberKey);
    }
}
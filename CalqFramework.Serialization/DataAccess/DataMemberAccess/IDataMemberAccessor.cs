using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public interface IDataMemberAccessor : IDataAccessor {
        MemberInfo? GetDataMember(string key);
        MemberInfo GetDataMemberOrThrow(string key);
        IDictionary<string, MemberInfo> GetDataMembersByKeys();
    }
}
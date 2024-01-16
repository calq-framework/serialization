using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public interface IDataMemberAccessor : IDataAccessor {
        object Obj { get; }
        bool TryGetDataMember(string key, out MemberInfo result);
        MemberInfo GetDataMember(string key);
        IDictionary<string, MemberInfo> GetDataMembersByKeys();
    }
}
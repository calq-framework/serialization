using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public interface IClassMemberAccessor : IDataAccessor<string, object>, IDataAccessor<MemberInfo, object> {
        object Obj { get; }
        bool HasDataMember(MemberInfo memberInfo);
        bool TryGetDataMember(string key, out MemberInfo result);
        MemberInfo GetDataMember(string key);
        string DataMemberToString(MemberInfo memberInfo);
        IDictionary<string, MemberInfo> GetDataMembersByKeys();
    }
}
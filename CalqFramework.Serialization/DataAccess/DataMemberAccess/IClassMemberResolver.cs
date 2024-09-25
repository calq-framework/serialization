using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {

    public interface IClassMemberResolver<TKey, TValue> : IMediaryKeyResolver<TKey, TValue> {
        object Obj { get; }
        bool HasDataMember(MemberInfo memberInfo);
        string DataMemberToString(MemberInfo memberInfo);
        IDictionary<TKey, MemberInfo> GetDataMembersByKeys();
    }
}
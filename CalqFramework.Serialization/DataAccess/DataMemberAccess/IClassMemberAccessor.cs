using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public interface IClassMemberAccessor : IDataAccessor<string, object> {
        object Obj { get; }
        bool HasDataMember(MemberInfo memberInfo);
        bool TryGetDataMember(string key, out MemberInfo result);
        MemberInfo GetDataMember(string key);
        string DataMemberToString(MemberInfo memberInfo);
        IDictionary<string, MemberInfo> GetDataMembersByKeys();

        bool Contains(MemberInfo member);

        public Type GetType(MemberInfo member);

        public object? GetValue(MemberInfo member);

        public object GetOrInitializeValue(MemberInfo member);

        public void SetValue(MemberInfo member, object? value);

        public bool SetOrAddValue(MemberInfo member, object? value);
    }
}
using System.Reflection;

namespace CalqFramework.Serialization;
public class DefaultDataMemberAccessor : DataMemberAccessorBase {

    public override MemberInfo GetDataMember(Type type, string dataMemberKey) {
        return  (MemberInfo?)type.GetField(dataMemberKey) ?? (MemberInfo?)type.GetProperty(dataMemberKey) ?? throw new MissingMemberException();
    }
}

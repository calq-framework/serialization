using Newtonsoft.Json.Linq;
using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.ClassMember;
sealed public class ClassDataMemberStoreFactory : ClassDataMemberStoreFactoryBase<string, object?>
{

    public static ClassDataMemberStoreFactory Instance { get; }

    static ClassDataMemberStoreFactory()
    {
        Instance = new ClassDataMemberStoreFactory(new ClassDataMemberStoreFactoryOptions());
    }

    public ClassDataMemberStoreFactory(ClassDataMemberStoreFactoryOptions classDataMemberStoreOptions) : base(classDataMemberStoreOptions)
    {
    }

    protected override IKeyValueStore<string, object?, MemberInfo> CreateFieldStore(object obj) {
        return new FieldStore(obj, ClassDataMemberStoreFactoryOptions.BindingAttr);
    }

    protected override IKeyValueStore<string, object?, MemberInfo> CreatePropertyStore(object obj) {
        return new PropertyStore(obj, ClassDataMemberStoreFactoryOptions.BindingAttr);
    }
}

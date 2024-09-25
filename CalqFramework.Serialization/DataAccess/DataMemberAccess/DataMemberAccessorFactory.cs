using Newtonsoft.Json.Linq;
using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess;
sealed public class DataMemberAccessorFactory : DataMemberAccessorFactoryBase<string, object?>
{

    public static DataMemberAccessorFactory Instance { get; }

    static DataMemberAccessorFactory()
    {
        Instance = new DataMemberAccessorFactory(new DataMemberAccessorOptions());
    }

    public DataMemberAccessorFactory(DataMemberAccessorOptions dataMemberAccessorOptions) : base(dataMemberAccessorOptions)
    {
    }

    protected override IDataAccessor<string, object?, MemberInfo> CreateFieldAccessor(object obj) {
        return new FieldAccessor(obj, DataMemberAccessorOptions.BindingAttr);
    }

    protected override IDataAccessor<string, object?, MemberInfo> CreatePropertyAccessor(object obj) {
        return new PropertyAccessor(obj, DataMemberAccessorOptions.BindingAttr);
    }
}

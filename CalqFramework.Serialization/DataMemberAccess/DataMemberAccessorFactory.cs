using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess;
public class DataMemberAccessorFactory {

    public static DataMemberAccessorFactory Instance { get; }

    public static IDataMemberAccessor DefaultDataMemberAccessor { get; }

    static DataMemberAccessorFactory() {
        Instance = new DataMemberAccessorFactory();
        DefaultDataMemberAccessor = Instance.CreateDataMemberAccessor(new DataMemberAccessorOptions());
    }

    protected DataMemberAccessorFactory() { }

    public IDataMemberAccessor CreateDataMemberAccessor(DataMemberAccessorOptions dataMemberAccessorOptions) {
        if (dataMemberAccessorOptions.AccessFields && dataMemberAccessorOptions.AccessProperties) {
            return CreateFieldAndPropertyAccessor(dataMemberAccessorOptions);
        } else if (dataMemberAccessorOptions.AccessFields) {
            return CreateFieldAccessor(dataMemberAccessorOptions);
        } else if (dataMemberAccessorOptions.AccessProperties) {
            return CreatePropertyAccessor(dataMemberAccessorOptions);
        } else {
            throw new ArgumentException("Neither AccessFields nor AccessProperties is set.");
        }
    }

    protected virtual IDataMemberAccessor CreateFieldAndPropertyAccessor(DataMemberAccessorOptions dataMemberAccessorOptions) {
        return new FieldAndPropertyAccessor(dataMemberAccessorOptions.BindingAttr);
    }
    protected virtual IDataMemberAccessor CreateFieldAccessor(DataMemberAccessorOptions dataMemberAccessorOptions) {
        return new FieldAccessor(dataMemberAccessorOptions.BindingAttr);
    }
    protected virtual IDataMemberAccessor CreatePropertyAccessor(DataMemberAccessorOptions dataMemberAccessorOptions) {
        return new PropertyAccessor(dataMemberAccessorOptions.BindingAttr);
    }
}

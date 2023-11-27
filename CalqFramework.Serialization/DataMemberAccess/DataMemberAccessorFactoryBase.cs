namespace CalqFramework.Serialization.DataMemberAccess;
public abstract class DataMemberAccessorFactoryBase {

    public virtual IDataMemberAccessor CreateDataMemberAccessor(DataMemberAccessorOptions dataMemberAccessorOptions) {
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

    protected IDataMemberAccessor CreateFieldAndPropertyAccessor(DataMemberAccessorOptions dataMemberAccessorOptions) {
        return new DualDataMemberAccessor(CreateFieldAccessor(dataMemberAccessorOptions), CreatePropertyAccessor(dataMemberAccessorOptions));
    }
    protected virtual IDataMemberAccessor CreateFieldAccessor(DataMemberAccessorOptions dataMemberAccessorOptions) {
        return new FieldAccessor(dataMemberAccessorOptions.BindingAttr);
    }
    protected virtual IDataMemberAccessor CreatePropertyAccessor(DataMemberAccessorOptions dataMemberAccessorOptions) {
        return new PropertyAccessor(dataMemberAccessorOptions.BindingAttr);
    }
}

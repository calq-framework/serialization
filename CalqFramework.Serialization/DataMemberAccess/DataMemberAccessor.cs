namespace CalqFramework.Serialization.DataMemberAccess;
public class DataMemberAccessor : DataMemberAccessorBase {

    public DataMemberAccessor() : this(new DataMemberAccessorOptions()) { }

    public DataMemberAccessor(DataMemberAccessorOptions dataMemberAccessorOptions) : base(
        dataMemberAccessorOptions.AccessFields && dataMemberAccessorOptions.AccessProperties ? new FieldAndPropertyResolver(dataMemberAccessorOptions.BindingAttr)
            : dataMemberAccessorOptions.AccessFields ? new FieldResolver(dataMemberAccessorOptions.BindingAttr)
            : dataMemberAccessorOptions.AccessProperties ? new PropertyResolver(dataMemberAccessorOptions.BindingAttr)
            : throw new ArgumentException("Neither AccessFields nor AccessProperties is set.")
    ) { }
}

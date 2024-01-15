namespace CalqFramework.Serialization.DataAccess.DataMemberAccess;
public abstract class DataMemberAccessorFactoryBase
{
    public DataMemberAccessorOptions DataMemberAccessorOptions { get; }

    public DataMemberAccessorFactoryBase(DataMemberAccessorOptions dataMemberAccessorOptions)
    {
        DataMemberAccessorOptions = dataMemberAccessorOptions;
    }

    public virtual IDataMemberAccessor CreateDataMemberAccessor(object obj)
    {
        if (DataMemberAccessorOptions.AccessFields && DataMemberAccessorOptions.AccessProperties)
        {
            return CreateFieldAndPropertyAccessor(obj);
        }
        else if (DataMemberAccessorOptions.AccessFields)
        {
            return CreateFieldAccessor(obj);
        }
        else if (DataMemberAccessorOptions.AccessProperties)
        {
            return CreatePropertyAccessor(obj);
        }
        else
        {
            throw new ArgumentException("Neither AccessFields nor AccessProperties is set.");
        }
    }

    protected DualDataMemberAccessor CreateFieldAndPropertyAccessor(object obj)
    {
        return new DualDataMemberAccessor(CreateFieldAccessor(obj), CreatePropertyAccessor(obj));
    }
    protected virtual FieldAccessorBase CreateFieldAccessor(object obj)
    {
        return new FieldAccessor(obj, DataMemberAccessorOptions.BindingAttr);
    }
    protected virtual PropertyAccessorBase CreatePropertyAccessor(object obj)
    {
        return new PropertyAccessor(obj, DataMemberAccessorOptions.BindingAttr);
    }
}

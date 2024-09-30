using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess;
public abstract class DataMemberAccessorFactoryBase<TKey, TValue> {

    public DataMemberAccessorOptions DataMemberAccessorOptions { get; }

    public DataMemberAccessorFactoryBase(DataMemberAccessorOptions dataMemberAccessorOptions)
    {
        DataMemberAccessorOptions = dataMemberAccessorOptions;
    }

    public virtual IDataAccessor<TKey, TValue> CreateDataMemberAccessor(object obj)
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

    protected IDataAccessor<TKey, TValue> CreateFieldAndPropertyAccessor(object obj)
    {
        return new DualDataAccessor<TKey, TValue>(CreateFieldAccessor(obj), CreatePropertyAccessor(obj));
    }
    protected abstract IDataAccessor<TKey, TValue> CreateFieldAccessor(object obj);
    protected abstract IDataAccessor<TKey, TValue> CreatePropertyAccessor(object obj);
}

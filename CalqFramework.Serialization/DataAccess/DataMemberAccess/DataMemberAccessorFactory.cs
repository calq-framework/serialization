namespace CalqFramework.Serialization.DataAccess.DataMemberAccess;
sealed public class DataMemberAccessorFactory : DataMemberAccessorFactoryBase
{

    public static DataMemberAccessorFactory Instance { get; }

    static DataMemberAccessorFactory()
    {
        Instance = new DataMemberAccessorFactory(new DataMemberAccessorOptions());
    }

    public DataMemberAccessorFactory(DataMemberAccessorOptions dataMemberAccessorOptions) : base(dataMemberAccessorOptions)
    {
    }
}

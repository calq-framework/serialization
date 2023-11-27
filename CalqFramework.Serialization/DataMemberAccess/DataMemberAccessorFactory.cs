namespace CalqFramework.Serialization.DataMemberAccess;
sealed public class DataMemberAccessorFactory : DataMemberAccessorFactoryBase {

    public static DataMemberAccessorFactory Instance { get; }

    public static IDataMemberAccessor DefaultDataMemberAccessor { get; }

    static DataMemberAccessorFactory() {
        Instance = new DataMemberAccessorFactory();
        DefaultDataMemberAccessor = Instance.CreateDataMemberAccessor(new DataMemberAccessorOptions());
    }
}

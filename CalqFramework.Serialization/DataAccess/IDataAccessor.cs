namespace CalqFramework.Serialization.DataAccess {

    public interface IDataAccessor<TKey, TValue> : IKeyedAccessor<TKey>  {
        public TValue this[TKey key] { get; set; }

        Type GetDataType(TKey key);

        TValue GetValueOrInitialize(TKey key);
    }

    public interface IDataAccessor<TKey, TValue, TDataMediator> : IDataMediatorResolver<TKey, TDataMediator>, IDataAccessor<TKey, TValue>, IDirectDataAccessor<TDataMediator, TValue> { }
}
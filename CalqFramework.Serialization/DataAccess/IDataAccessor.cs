namespace CalqFramework.Serialization.DataAccess {

    public interface IDataAccessor<TKey, TValue> : IKeyedAccessor<TKey>  {
        Type GetType(TKey key);

        TValue GetValue(TKey key);

        TValue GetValueOrInitialize(TKey key);

        void SetValue(TKey key, TValue value);
    }

    public interface IDataAccessor<TKey, TValue, TDataMediator> : IDataMediatorResolver<TKey, TDataMediator>, IDataAccessor<TKey, TValue>, IDirectDataAccessor<TDataMediator, TValue> { }
}
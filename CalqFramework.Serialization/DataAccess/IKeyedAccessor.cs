namespace CalqFramework.Serialization.DataAccess {
    public interface IKeyedAccessor<TKey> {
        bool ContainsKey(TKey key);
    }

    public interface IKeyedAccessor<TKey, TDataMediator> : IKeyedAccessor<TKey> {
        bool ContainsDataMediator(TDataMediator key);
    }
}
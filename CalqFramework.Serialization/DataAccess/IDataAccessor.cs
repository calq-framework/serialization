using System.Reflection;

namespace CalqFramework.Serialization.DataAccess {
    public interface IDataAccessor<TKey, TValue> {
        bool Contains(TKey key);

        Type GetType(TKey key);

        object? GetValue(TKey key);

        object GetOrInitializeValue(TKey key);

        void SetValue(TKey key, TValue? value);
    }

    public interface IDataAccessor<TKey, TValue, TDataMediator> : IDataMediatorResolver<TKey, TDataMediator>, IDataAccessor<TKey, TValue>, IDirectDataAccessor<TDataMediator, TValue> { }
}
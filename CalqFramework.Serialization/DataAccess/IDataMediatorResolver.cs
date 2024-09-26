using System.Diagnostics.CodeAnalysis;

namespace CalqFramework.Serialization.DataAccess {
    public interface IDataMediatorResolver<TKey, TDataMediator> : IKeyedAccessor<TKey, TDataMediator> {
        bool TryGetDataMediator(TKey key, [MaybeNullWhen(false)] out TDataMediator result);
        TDataMediator GetDataMediator(TKey key);
    }
}
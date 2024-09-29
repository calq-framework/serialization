using System.Diagnostics.CodeAnalysis;

namespace CalqFramework.Serialization.DataAccess {
    public interface IDataMediatorResolver<TKey, TDataMediator> : IKeyedAccessor<TKey, TDataMediator> {
        protected bool TryGetDataMediator(TKey key, [MaybeNullWhen(false)] out TDataMediator result);
        protected TDataMediator GetDataMediator(TKey key);
    }
}
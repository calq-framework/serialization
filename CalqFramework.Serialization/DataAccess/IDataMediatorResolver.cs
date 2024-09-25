using System.Reflection;

namespace CalqFramework.Serialization.DataAccess {
    public interface IDataMediatorResolver<TKey, TDataMediator> {
        bool TryGetDataMediator(TKey key, out TDataMediator result);
        TDataMediator GetDataMediator(TKey key);
    }
}
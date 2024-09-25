using System.Reflection;

namespace CalqFramework.Serialization.DataAccess {
    public interface IDirectDataAccessor<TDataMediator, TValue> {
        IEnumerable<TDataMediator> DataMediators { get; }

        string DataMediatorToString(TDataMediator dataMediator);

        bool Contains(TDataMediator dataMediator);

        Type GetType(TDataMediator dataMediator);

        object? GetValue(TDataMediator dataMediator);

        object GetOrInitializeValue(TDataMediator dataMediator);

        void SetValue(TDataMediator dataMediator, TValue? value);
    }
}
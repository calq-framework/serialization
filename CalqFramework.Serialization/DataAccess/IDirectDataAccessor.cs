namespace CalqFramework.Serialization.DataAccess {
    public interface IDirectDataAccessor<TDataMediator, TValue> {
        IEnumerable<TDataMediator> DataMediators { get; }

        string DataMediatorToString(TDataMediator dataMediator);

        Type GetType(TDataMediator dataMediator);

        TValue GetValue(TDataMediator dataMediator);

        TValue GetValueOrInitialize(TDataMediator dataMediator);

        void SetValue(TDataMediator dataMediator, TValue value);
    }
}
namespace CalqFramework.Serialization.DataAccess {
    public interface IDirectDataAccessor<TDataMediator, TValue> {
        public TValue this[TDataMediator dataMediator] { get; set; }

        IEnumerable<TDataMediator> DataMediators { get; }

        string DataMediatorToString(TDataMediator dataMediator);

        Type GetDataType(TDataMediator dataMediator);

        TValue GetValueOrInitialize(TDataMediator dataMediator);
    }
}
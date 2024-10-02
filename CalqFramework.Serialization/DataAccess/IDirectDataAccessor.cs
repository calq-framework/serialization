namespace CalqFramework.Serialization.DataAccess {
    public interface IDirectDataAccessor<TDataMediator, TValue> {
        protected internal TValue this[TDataMediator dataMediator] { get; set; }

        protected internal IEnumerable<TDataMediator> DataMediators { get; }

        protected internal string DataMediatorToString(TDataMediator dataMediator);

        protected internal Type GetDataType(TDataMediator dataMediator);

        protected internal TValue GetValueOrInitialize(TDataMediator dataMediator);
    }
}
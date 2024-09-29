namespace CalqFramework.Serialization.DataAccess {
    public interface IDirectDataAccessor<TDataMediator, TValue> {
        protected TValue this[TDataMediator dataMediator] { get; set; }

        protected IEnumerable<TDataMediator> DataMediators { get; }

        protected string DataMediatorToString(TDataMediator dataMediator);

        protected Type GetDataType(TDataMediator dataMediator);

        protected TValue GetValueOrInitialize(TDataMediator dataMediator);
    }
}
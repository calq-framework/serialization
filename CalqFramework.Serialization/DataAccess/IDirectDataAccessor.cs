namespace CalqFramework.Serialization.DataAccess {
    public interface IDirectDataAccessor<TDataMediator, TValue> {
        bool Contains(TDataMediator dataMediator);

        public Type GetType(TDataMediator dataMediator);

        public object? GetValue(TDataMediator dataMediator);

        public object GetOrInitializeValue(TDataMediator dataMediator);

        public void SetValue(TDataMediator dataMediator, TValue? value);

        public bool SetOrAddValue(TDataMediator dataMediator, TValue? value);
    }
}
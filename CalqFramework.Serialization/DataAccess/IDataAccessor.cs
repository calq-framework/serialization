namespace CalqFramework.Serialization.DataAccess {
    public interface IDataAccessor<TKey, TValue> {
        bool Contains(TKey key);

        public Type GetType(TKey key);

        public object? GetValue(TKey key);

        public object GetOrInitializeValue(TKey key);

        public void SetValue(TKey key, TValue? value);

        public bool SetOrAddValue(TKey key, TValue? value);
    }
}
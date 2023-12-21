namespace CalqFramework.Serialization.DataAccess {
    public interface IDataAccessor {
        public Type GetType(string key);

        public object? GetValue(string key);

        public object GetOrInitializeValue(string key);

        public void SetValue(string key, object? value);
    }
}
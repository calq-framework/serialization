namespace CalqFramework.Serialization.DataAccess {
    public interface IDataAccessor {
        bool HasKey(string key);

        public Type GetType(string key);

        public object? GetValue(string key);

        public object GetOrInitializeValue(string key);

        public void SetValue(string key, object? value);

        public void SetOrAddValue(string key, object? value);
    }
}
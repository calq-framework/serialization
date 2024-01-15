namespace CalqFramework.Serialization.DataAccess {
    public class DualDataAccessor : IDataAccessor
    {
        public DataAccessorBase PrimaryAccessor { get; }
        public DataAccessorBase SecondaryAccessor { get; }

        public DualDataAccessor(DataAccessorBase primaryAccessor, DataAccessorBase secondaryAccessor)
        {
            PrimaryAccessor = primaryAccessor;
            SecondaryAccessor = secondaryAccessor;
        }

        private void AssertNoCollision(string key) {
            if (PrimaryAccessor.HasKey(key) && SecondaryAccessor.HasKey(key)) {
                throw new Exception("collision");
            }
        }

        public Type GetType(string key)
        {
            AssertNoCollision(key);

            if (PrimaryAccessor.HasKey(key))
            {
                return PrimaryAccessor.GetType(key);
            }
            else
            {
                return SecondaryAccessor.GetType(key);
            }
        }

        public object? GetValue(string key)
        {
            AssertNoCollision(key);

            if (PrimaryAccessor.HasKey(key))
            {
                return PrimaryAccessor.GetValue(key);
            }
            else
            {
                return SecondaryAccessor.GetValue(key);
            }
        }

        public object GetOrInitializeValue(string key)
        {
            AssertNoCollision(key);

            if (PrimaryAccessor.HasKey(key))
            {
                return PrimaryAccessor.GetOrInitializeValue(key);
            }
            else
            {
                return SecondaryAccessor.GetOrInitializeValue(key);
            }
        }

        public void SetValue(string key, object? value)
        {
            AssertNoCollision(key);

            if (PrimaryAccessor.HasKey(key))
            {
                PrimaryAccessor.SetValue(key, value);
            }
            else
            {
                SecondaryAccessor.SetValue(key, value);
            }
        }

        public bool HasKey(string key)
        {
            AssertNoCollision(key);

            if (PrimaryAccessor.HasKey(key))
            {
                return PrimaryAccessor.HasKey(key);
            }
            else
            {
                return SecondaryAccessor.HasKey(key);
            }
        }

        public void SetOrAddValue(string key, object? value) {
            AssertNoCollision(key);

            if (PrimaryAccessor.HasKey(key)) {
                PrimaryAccessor.SetOrAddValue(key, value);
            } else {
                SecondaryAccessor.SetOrAddValue(key, value);
            }
        }
    }
}
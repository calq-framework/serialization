namespace CalqFramework.Serialization.DataAccess {
    public class DualDataAccessor : IDataAccessor
    {
        public IDataAccessor PrimaryAccessor { get; }
        public IDataAccessor SecondaryAccessor { get; }

        public DualDataAccessor(IDataAccessor primaryAccessor, IDataAccessor secondaryAccessor)
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

        public bool SetOrAddValue(string key, object? value) {
            AssertNoCollision(key);

            if (PrimaryAccessor.HasKey(key)) {
                return PrimaryAccessor.SetOrAddValue(key, value);
            } else {
                return SecondaryAccessor.SetOrAddValue(key, value);
            }
        }
    }
}
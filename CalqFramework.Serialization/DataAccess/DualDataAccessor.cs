namespace CalqFramework.Serialization.DataAccess {
    public class DualDataAccessor<TKey, TValue> : IDataAccessor<TKey, TValue>
    {
        public IDataAccessor<TKey, TValue> PrimaryAccessor { get; }
        public IDataAccessor<TKey, TValue> SecondaryAccessor { get; }

        public DualDataAccessor(IDataAccessor<TKey, TValue> primaryAccessor, IDataAccessor<TKey, TValue> secondaryAccessor)
        {
            PrimaryAccessor = primaryAccessor;
            SecondaryAccessor = secondaryAccessor;
        }

        private void AssertNoCollision(TKey key) {
            if (PrimaryAccessor.HasKey(key) && SecondaryAccessor.HasKey(key)) {
                throw new Exception("collision");
            }
        }

        public Type GetType(TKey key)
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

        public object? GetValue(TKey key)
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

        public object GetOrInitializeValue(TKey key)
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

        public void SetValue(TKey key, TValue? value)
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

        public bool HasKey(TKey key)
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

        public bool SetOrAddValue(TKey key, TValue? value) {
            AssertNoCollision(key);

            if (PrimaryAccessor.HasKey(key)) {
                return PrimaryAccessor.SetOrAddValue(key, value);
            } else {
                return SecondaryAccessor.SetOrAddValue(key, value);
            }
        }
    }
}
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
            if (PrimaryAccessor.Contains(key) && SecondaryAccessor.Contains(key)) {
                throw new Exception("collision");
            }
        }

        public Type GetType(TKey key)
        {
            AssertNoCollision(key);

            if (PrimaryAccessor.Contains(key))
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

            if (PrimaryAccessor.Contains(key))
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

            if (PrimaryAccessor.Contains(key))
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

            if (PrimaryAccessor.Contains(key))
            {
                PrimaryAccessor.SetValue(key, value);
            }
            else
            {
                SecondaryAccessor.SetValue(key, value);
            }
        }

        public bool Contains(TKey key)
        {
            AssertNoCollision(key);

            if (PrimaryAccessor.Contains(key))
            {
                return PrimaryAccessor.Contains(key);
            }
            else
            {
                return SecondaryAccessor.Contains(key);
            }
        }

        public bool SetOrAddValue(TKey key, TValue? value) {
            AssertNoCollision(key);

            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.SetOrAddValue(key, value);
            } else {
                return SecondaryAccessor.SetOrAddValue(key, value);
            }
        }
    }

    public class DualDataAccessor<TKey, TValue, TMediaryKey> : IDataAccessor<TKey, TValue, TMediaryKey> {
        public IDataAccessor<TKey, TValue, TMediaryKey> PrimaryAccessor { get; }
        public IDataAccessor<TKey, TValue, TMediaryKey> SecondaryAccessor { get; }

        public DualDataAccessor(IDataAccessor<TKey, TValue, TMediaryKey> primaryAccessor, IDataAccessor<TKey, TValue, TMediaryKey> secondaryAccessor) {
            PrimaryAccessor = primaryAccessor;
            SecondaryAccessor = secondaryAccessor;
        }

        private void AssertNoCollision(TKey key) {
            if (PrimaryAccessor.Contains(key) && SecondaryAccessor.Contains(key)) {
                throw new Exception("collision");
            }
        }

        public Type GetType(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.GetType(key);
            } else {
                return SecondaryAccessor.GetType(key);
            }
        }

        public object? GetValue(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.GetValue(key);
            } else {
                return SecondaryAccessor.GetValue(key);
            }
        }

        public object GetOrInitializeValue(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.GetOrInitializeValue(key);
            } else {
                return SecondaryAccessor.GetOrInitializeValue(key);
            }
        }

        public void SetValue(TKey key, TValue? value) {
            AssertNoCollision(key);

            if (PrimaryAccessor.Contains(key)) {
                PrimaryAccessor.SetValue(key, value);
            } else {
                SecondaryAccessor.SetValue(key, value);
            }
        }

        public bool Contains(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.Contains(key);
            } else {
                return SecondaryAccessor.Contains(key);
            }
        }

        public bool SetOrAddValue(TKey key, TValue? value) {
            AssertNoCollision(key);

            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.SetOrAddValue(key, value);
            } else {
                return SecondaryAccessor.SetOrAddValue(key, value);
            }
        }

        public Type GetType(TMediaryKey key) {
            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.GetType(key);
            } else {
                return SecondaryAccessor.GetType(key);
            }
        }

        public object? GetValue(TMediaryKey key) {
            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.GetValue(key);
            } else {
                return SecondaryAccessor.GetValue(key);
            }
        }

        public object GetOrInitializeValue(TMediaryKey key) {
            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.GetOrInitializeValue(key);
            } else {
                return SecondaryAccessor.GetOrInitializeValue(key);
            }
        }

        public void SetValue(TMediaryKey key, TValue? value) {
            if (PrimaryAccessor.Contains(key)) {
                PrimaryAccessor.SetValue(key, value);
            } else {
                SecondaryAccessor.SetValue(key, value);
            }
        }

        public bool Contains(TMediaryKey key) {
            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.Contains(key);
            } else {
                return SecondaryAccessor.Contains(key);
            }
        }

        public bool SetOrAddValue(TMediaryKey key, TValue? value) {
            if (PrimaryAccessor.Contains(key)) {
                return PrimaryAccessor.SetOrAddValue(key, value);
            } else {
                return SecondaryAccessor.SetOrAddValue(key, value);
            }
        }

        public bool TryGetMediaryKey(TKey key, out TMediaryKey result) {
            PrimaryAccessor.TryGetMediaryKey(key, out result);
            if (result != null) {
                return true;
            }
            SecondaryAccessor.TryGetMediaryKey(key, out result);
            if (result != null) {
                return true;
            }
            return false;
        }

        public TMediaryKey GetMediaryKey(TKey key) {
            return PrimaryAccessor.GetMediaryKey(key) ?? SecondaryAccessor.GetMediaryKey(key); ;
        }
    }
}
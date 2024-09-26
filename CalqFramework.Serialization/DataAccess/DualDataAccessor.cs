using System.Diagnostics.CodeAnalysis;

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
    }

    public class DualDataAccessor<TKey, TValue, TDataMediator> : IDataAccessor<TKey, TValue, TDataMediator> {
        public IDataAccessor<TKey, TValue, TDataMediator> PrimaryAccessor { get; }
        public IDataAccessor<TKey, TValue, TDataMediator> SecondaryAccessor { get; }

        public IEnumerable<TDataMediator> DataMediators => PrimaryAccessor.DataMediators.Concat(SecondaryAccessor.DataMediators);

        public DualDataAccessor(IDataAccessor<TKey, TValue, TDataMediator> primaryAccessor, IDataAccessor<TKey, TValue, TDataMediator> secondaryAccessor) {
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

        public Type GetType(TDataMediator dataMediator) {
            if (PrimaryAccessor.Contains(dataMediator)) {
                return PrimaryAccessor.GetType(dataMediator);
            } else {
                return SecondaryAccessor.GetType(dataMediator);
            }
        }

        public object? GetValue(TDataMediator dataMediator) {
            if (PrimaryAccessor.Contains(dataMediator)) {
                return PrimaryAccessor.GetValue(dataMediator);
            } else {
                return SecondaryAccessor.GetValue(dataMediator);
            }
        }

        public object GetOrInitializeValue(TDataMediator dataMediator) {
            if (PrimaryAccessor.Contains(dataMediator)) {
                return PrimaryAccessor.GetOrInitializeValue(dataMediator);
            } else {
                return SecondaryAccessor.GetOrInitializeValue(dataMediator);
            }
        }

        public void SetValue(TDataMediator dataMediator, TValue? value) {
            if (PrimaryAccessor.Contains(dataMediator)) {
                PrimaryAccessor.SetValue(dataMediator, value);
            } else {
                SecondaryAccessor.SetValue(dataMediator, value);
            }
        }

        public bool Contains(TDataMediator dataMediator) {
            if (PrimaryAccessor.Contains(dataMediator)) {
                return PrimaryAccessor.Contains(dataMediator);
            } else {
                return SecondaryAccessor.Contains(dataMediator);
            }
        }

        public bool TryGetDataMediator(TKey key, [MaybeNullWhen(false)] out TDataMediator result) {
            PrimaryAccessor.TryGetDataMediator(key, out result);
            if (result == null) {
                SecondaryAccessor.TryGetDataMediator(key, out result);
            }
            return result != null;
        }

        public TDataMediator GetDataMediator(TKey key) {
            return PrimaryAccessor.GetDataMediator(key) ?? SecondaryAccessor.GetDataMediator(key); ;
        }

        public string DataMediatorToString(TDataMediator dataMediator) {
            if (PrimaryAccessor.Contains(dataMediator)) {
                return PrimaryAccessor.DataMediatorToString(dataMediator);
            } else {
                return SecondaryAccessor.DataMediatorToString(dataMediator);
            }
        }
    }
}
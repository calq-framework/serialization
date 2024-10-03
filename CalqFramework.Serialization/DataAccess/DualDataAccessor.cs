using System.Diagnostics.CodeAnalysis;

namespace CalqFramework.Serialization.DataAccess {
    public class DualDataAccessor<TKey, TValue> : IDataAccessor<TKey, TValue> {
        public IDataAccessor<TKey, TValue> PrimaryAccessor { get; }
        public IDataAccessor<TKey, TValue> SecondaryAccessor { get; }

        public virtual TValue this[TKey key] {
            get {
                AssertNoCollision(key);

                if (PrimaryAccessor.ContainsKey(key)) {
                    return PrimaryAccessor[key];
                } else {
                    return SecondaryAccessor[key];
                }
            }
            set {
                AssertNoCollision(key);

                if (PrimaryAccessor.ContainsKey(key)) {
                    PrimaryAccessor[key] = value;
                } else {
                    SecondaryAccessor[key] = value;
                }
            }
        }

        public DualDataAccessor(IDataAccessor<TKey, TValue> primaryAccessor, IDataAccessor<TKey, TValue> secondaryAccessor) {
            PrimaryAccessor = primaryAccessor;
            SecondaryAccessor = secondaryAccessor;
        }

        private void AssertNoCollision(TKey key) {
            if (PrimaryAccessor.ContainsKey(key) && SecondaryAccessor.ContainsKey(key)) {
                throw new Exception("collision");
            }
        }

        public Type GetDataType(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.ContainsKey(key)) {
                return PrimaryAccessor.GetDataType(key);
            } else {
                return SecondaryAccessor.GetDataType(key);
            }
        }

        public TValue GetValueOrInitialize(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.ContainsKey(key)) {
                return PrimaryAccessor.GetValueOrInitialize(key);
            } else {
                return SecondaryAccessor.GetValueOrInitialize(key);
            }
        }

        public bool ContainsKey(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.ContainsKey(key)) {
                return PrimaryAccessor.ContainsKey(key);
            } else {
                return SecondaryAccessor.ContainsKey(key);
            }
        }
    }

    public class DualDataAccessor<TKey, TValue, TDataMediator> : IDataAccessor<TKey, TValue, TDataMediator> {
        public IDataAccessor<TKey, TValue, TDataMediator> PrimaryAccessor { get; }
        public IDataAccessor<TKey, TValue, TDataMediator> SecondaryAccessor { get; }

        public IEnumerable<TDataMediator> DataMediators => PrimaryAccessor.DataMediators.Concat(SecondaryAccessor.DataMediators);

        public TValue this[TDataMediator dataMediator] {
            get {
                if (PrimaryAccessor.ContainsDataMediator(dataMediator)) {
                    return PrimaryAccessor[dataMediator];
                } else {
                    return SecondaryAccessor[dataMediator];
                }
            }
            set {
                if (PrimaryAccessor.ContainsDataMediator(dataMediator)) {
                    PrimaryAccessor[dataMediator] = value;
                } else {
                    SecondaryAccessor[dataMediator] = value;
                }
            }
        }

        public virtual TValue this[TKey key] {
            get {
                AssertNoCollision(key);

                if (PrimaryAccessor.ContainsKey(key)) {
                    return PrimaryAccessor[key];
                } else {
                    return SecondaryAccessor[key];
                }
            }
            set {
                AssertNoCollision(key);

                if (PrimaryAccessor.ContainsKey(key)) {
                    PrimaryAccessor[key] = value;
                } else {
                    SecondaryAccessor[key] = value;
                }
            }
        }

        public DualDataAccessor(IDataAccessor<TKey, TValue, TDataMediator> primaryAccessor, IDataAccessor<TKey, TValue, TDataMediator> secondaryAccessor) {
            PrimaryAccessor = primaryAccessor;
            SecondaryAccessor = secondaryAccessor;
        }

        private void AssertNoCollision(TKey key) {
            if (PrimaryAccessor.ContainsKey(key) && SecondaryAccessor.ContainsKey(key)) {
                throw new Exception("collision");
            }
        }

        public Type GetDataType(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.ContainsKey(key)) {
                return PrimaryAccessor.GetDataType(key);
            } else {
                return SecondaryAccessor.GetDataType(key);
            }
        }

        public TValue GetValueOrInitialize(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.ContainsKey(key)) {
                return PrimaryAccessor.GetValueOrInitialize(key);
            } else {
                return SecondaryAccessor.GetValueOrInitialize(key);
            }
        }

        public bool ContainsKey(TKey key) {
            AssertNoCollision(key);

            if (PrimaryAccessor.ContainsKey(key)) {
                return PrimaryAccessor.ContainsKey(key);
            } else {
                return SecondaryAccessor.ContainsKey(key);
            }
        }

        public Type GetDataType(TDataMediator dataMediator) {
            if (PrimaryAccessor.ContainsDataMediator(dataMediator)) {
                return PrimaryAccessor.GetDataType(dataMediator);
            } else {
                return SecondaryAccessor.GetDataType(dataMediator);
            }
        }

        public TValue GetValueOrInitialize(TDataMediator dataMediator) {
            if (PrimaryAccessor.ContainsDataMediator(dataMediator)) {
                return PrimaryAccessor.GetValueOrInitialize(dataMediator);
            } else {
                return SecondaryAccessor.GetValueOrInitialize(dataMediator);
            }
        }

        public bool ContainsDataMediator(TDataMediator dataMediator) {
            if (PrimaryAccessor.ContainsDataMediator(dataMediator)) {
                return PrimaryAccessor.ContainsDataMediator(dataMediator);
            } else {
                return SecondaryAccessor.ContainsDataMediator(dataMediator);
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
            if (PrimaryAccessor.ContainsDataMediator(dataMediator)) {
                return PrimaryAccessor.DataMediatorToString(dataMediator);
            } else {
                return SecondaryAccessor.DataMediatorToString(dataMediator);
            }
        }
    }
}
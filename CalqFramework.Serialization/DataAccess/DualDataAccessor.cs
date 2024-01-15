using CalqFramework.Serialization.DataAccess.DataMemberAccess;

namespace CalqFramework.Serialization.DataAccess {
    public class DualDataAccessor : IDataAccessor
    {
        public IDataAccessor PrimaryAccessor { get; }
        public IDataAccessor SecondaryAccessor { get; }

        public DualDataAccessor(DataMemberAccessorBase primaryAccessor, DataMemberAccessorBase secondaryAccessor)
        {
            PrimaryAccessor = primaryAccessor;
            SecondaryAccessor = secondaryAccessor;
        }

        public Type GetType(string key)
        {
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
            if (PrimaryAccessor.HasKey(key))
            {
                return PrimaryAccessor.HasKey(key);
            }
            else
            {
                return SecondaryAccessor.HasKey(key);
            }
        }
    }
}
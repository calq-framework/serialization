using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public class DualDataMemberAccessor : IDataMemberAccessor
    {
        public DataMemberAccessorBase PrimaryAccessor { get; }
        public DataMemberAccessorBase SecondaryAccessor { get; }

        public DualDataMemberAccessor(DataMemberAccessorBase primaryAccessor, DataMemberAccessorBase secondaryAccessor) {
            PrimaryAccessor = primaryAccessor;
            SecondaryAccessor = secondaryAccessor;
        }

        public IDictionary<string, MemberInfo> GetDataMembersByKeys()
        {
            var primaryResult = PrimaryAccessor.GetDataMembersByKeys();
            var secondaryResult = SecondaryAccessor.GetDataMembersByKeys();
            var result = primaryResult.Concat(secondaryResult).ToDictionary(kv => kv.Key, kv => kv.Value);
            return result;
        }

        public MemberInfo? GetDataMember(string key)
        {
            return PrimaryAccessor.GetDataMember(key) ?? SecondaryAccessor.GetDataMember(key);
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

        public bool HasKey(string key) {
            if (PrimaryAccessor.HasKey(key)) {
                return PrimaryAccessor.HasKey(key);
            } else {
                return SecondaryAccessor.HasKey(key);
            }
        }

        public MemberInfo GetDataMemberOrThrow(string key) {
            if (PrimaryAccessor.HasKey(key)) {
                return PrimaryAccessor.GetDataMemberOrThrow(key);
            } else {
                return SecondaryAccessor.GetDataMemberOrThrow(key);
            }
        }

        public void SetOrAddValue(string key, object? value) {
            if (PrimaryAccessor.HasKey(key)) {
                PrimaryAccessor.SetOrAddValue(key, value);
            } else {
                SecondaryAccessor.SetOrAddValue(key, value);
            }
        }
    }
}
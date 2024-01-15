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

        public MemberInfo GetDataMember(string key) {
            if (PrimaryAccessor.TryGetDataMember(key, out var primaryDataMember)) {
                return primaryDataMember;
            }
            return SecondaryAccessor.GetDataMember(key);
        }


        public bool TryGetDataMember(string key, out MemberInfo result)
        {
            if (PrimaryAccessor.TryGetDataMember(key, out var primaryDataMember)) {
                result = primaryDataMember;
                return true;
            }
            else if (SecondaryAccessor.TryGetDataMember(key, out var secondaryDataMember)) {
                result = secondaryDataMember;
                return true;
            }
            result = null!;
            return false;
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

        public void SetOrAddValue(string key, object? value) {
            if (PrimaryAccessor.HasKey(key)) {
                PrimaryAccessor.SetOrAddValue(key, value);
            } else {
                SecondaryAccessor.SetOrAddValue(key, value);
            }
        }
    }
}
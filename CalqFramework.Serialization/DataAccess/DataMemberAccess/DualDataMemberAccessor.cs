using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    internal class DualDataMemberAccessor : IDataAccessor
    {
        public DataMemberAccessorBase PrimaryAccessor { get; }
        public DataMemberAccessorBase SecondaryAccessor { get; }
       
        public DualDataMemberAccessor(DataMemberAccessorBase primaryAccessor, DataMemberAccessorBase secondaryAccessor)
        {
            PrimaryAccessor = primaryAccessor;
            SecondaryAccessor = secondaryAccessor;
        }

        public IDictionary<string, MemberInfo> GetDataMembersByKeys()
        {
            return PrimaryAccessor.GetDataMembersByKeys() ?? SecondaryAccessor.GetDataMembersByKeys();
        }

        public MemberInfo? GetDataMember(string dataMemberKey)
        {
            return PrimaryAccessor.GetDataMember(dataMemberKey) ?? SecondaryAccessor.GetDataMember(dataMemberKey);
        }

        public Type GetType(string dataMemberKey)
        {
            if (PrimaryAccessor.GetDataMember(dataMemberKey) != null)
            {
                return PrimaryAccessor.GetType(dataMemberKey);
            }
            else
            {
                return SecondaryAccessor.GetType(dataMemberKey);
            }
        }

        public object? GetValue(string dataMemberKey)
        {
            if (PrimaryAccessor.GetDataMember(dataMemberKey) != null)
            {
                return PrimaryAccessor.GetValue(dataMemberKey);
            }
            else
            {
                return SecondaryAccessor.GetValue(dataMemberKey);
            }
        }

        public object GetOrInitializeValue(string dataMemberKey)
        {
            if (PrimaryAccessor.GetDataMember(dataMemberKey) != null)
            {
                return PrimaryAccessor.GetOrInitializeValue(dataMemberKey);
            }
            else
            {
                return SecondaryAccessor.GetOrInitializeValue(dataMemberKey);
            }
        }

        public void SetValue(string dataMemberKey, object? value)
        {
            if (PrimaryAccessor.GetDataMember(dataMemberKey) != null)
            {
                PrimaryAccessor.SetValue(dataMemberKey, value);
            }
            else
            {
                SecondaryAccessor.SetValue(dataMemberKey, value);
            }
        }

        public bool HasKey(string dataMemberKey) {
            if (PrimaryAccessor.GetDataMember(dataMemberKey) != null) {
                return PrimaryAccessor.HasKey(dataMemberKey);
            } else {
                return SecondaryAccessor.HasKey(dataMemberKey);
            }
        }
    }
}
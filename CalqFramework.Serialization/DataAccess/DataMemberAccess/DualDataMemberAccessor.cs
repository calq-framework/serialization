using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public class DualDataMemberAccessor : IDataMemberAccessor
    {
        public IDataMemberAccessor PrimaryAccessor { get; }
        public IDataMemberAccessor SecondaryAccessor { get; }

        public object Obj => PrimaryAccessor.Obj;

        public DualDataMemberAccessor(IDataMemberAccessor primaryAccessor, IDataMemberAccessor secondaryAccessor) {
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

        public bool HasDataMember(MemberInfo memberInfo) {
            return PrimaryAccessor.HasDataMember(memberInfo) || SecondaryAccessor.HasDataMember(memberInfo);
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

        public string DataMemberToString(MemberInfo memberInfo) {
            if (PrimaryAccessor.HasDataMember(memberInfo)) {
                return PrimaryAccessor.DataMemberToString(memberInfo);
            }
            return SecondaryAccessor.DataMemberToString(memberInfo);
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
            return PrimaryAccessor.HasKey(key) || SecondaryAccessor.HasKey(key);
        }

        public bool SetOrAddValue(string key, object? value) {
            if (PrimaryAccessor.HasKey(key)) {
                return PrimaryAccessor.SetOrAddValue(key, value);
            } else {
                return SecondaryAccessor.SetOrAddValue(key, value);
            }
        }

        public bool Contains(MemberInfo member) {
            return PrimaryAccessor.Contains(member) || SecondaryAccessor.Contains(member);
        }

        public Type GetType(MemberInfo member) {
            if (PrimaryAccessor.Contains(member)) {
                return PrimaryAccessor.GetType(member);
            }
            return SecondaryAccessor.GetType(member);
        }

        public object? GetValue(MemberInfo member) {
            if (PrimaryAccessor.Contains(member)) {
                return PrimaryAccessor.GetValue(member);
            }
            return SecondaryAccessor.GetValue(member);
        }

        public object GetOrInitializeValue(MemberInfo member) {
            if (PrimaryAccessor.Contains(member)) {
                return PrimaryAccessor.GetOrInitializeValue(member);
            }
            return SecondaryAccessor.GetOrInitializeValue(member);
        }

        public void SetValue(MemberInfo member, object? value) {
            if (PrimaryAccessor.Contains(member)) {
                PrimaryAccessor.SetValue(member, value);
            }
            SecondaryAccessor.SetValue(member, value);
        }

        public bool SetOrAddValue(MemberInfo member, object? value) {
            if (PrimaryAccessor.Contains(member)) {
                return PrimaryAccessor.SetOrAddValue(member, value);
            }
            return SecondaryAccessor.SetOrAddValue(member, value);
        }
    }
}
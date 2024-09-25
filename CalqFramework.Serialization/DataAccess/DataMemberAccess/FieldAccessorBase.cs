using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public abstract class FieldAccessorBase<TKey> : ClassMemberResolverBase<TKey, object?>, IDataAccessor<TKey, object?, MemberInfo> {
        public FieldAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        public bool Contains(MemberInfo key) {
            return key is FieldInfo;
        }

        public Type GetType(MemberInfo key) {
            return ((FieldInfo)key).FieldType;
        }

        public object? GetValue(MemberInfo key) {
            return ((FieldInfo)key).GetValue(Obj);
        }

        public object GetOrInitializeValue(MemberInfo key) {
            var value = ((FieldInfo)key).GetValue(Obj) ??
                   Activator.CreateInstance(((FieldInfo)key).FieldType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)key).FieldType)!)!;
            ((FieldInfo)key).SetValue(Obj, value);
            return value;
        }

        public void SetValue(MemberInfo key, object? value) {
            ((FieldInfo)key).SetValue(Obj, value);
        }

        public bool SetOrAddValue(MemberInfo key, object? value) {
            throw new NotImplementedException();
        }

        public override  string DataMemberToString(MemberInfo memberInfo) {
            throw new NotImplementedException();
        }

        public override IDictionary<TKey, MemberInfo> GetDataMembersByKeys() {
            throw new NotImplementedException();
        }

        public object GetOrInitializeValue(TKey key) {
            var dataMember = GetMediaryKey(key);
            return GetOrInitializeValue(dataMember);
        }

        public Type GetType(TKey key) {
            var dataMember = GetMediaryKey(key);
            return GetType(dataMember);
        }

        public object? GetValue(TKey key) {
            var dataMember = GetMediaryKey(key);
            return GetOrInitializeValue(dataMember);
        }

        public override bool HasDataMember(MemberInfo memberInfo) {
            throw new NotImplementedException();
        }

        public bool SetOrAddValue(TKey key, object? value) {
            var dataMember = GetMediaryKey(key);
            return SetOrAddValue(dataMember, value);
        }

        public void SetValue(TKey key, object? value) {
            var dataMember = GetMediaryKey(key);
            SetValue(dataMember, value);
        }
    }
}
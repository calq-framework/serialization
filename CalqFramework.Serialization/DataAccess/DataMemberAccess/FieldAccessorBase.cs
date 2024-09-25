using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public abstract class FieldAccessorBase<TKey> : ClassMemberResolverBase<TKey, object?>, IDataAccessor<TKey, object?, MemberInfo> {
        public FieldAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        public bool Contains(MemberInfo dataMediator) {
            return dataMediator is FieldInfo;
        }

        public Type GetType(MemberInfo dataMediator) {
            return ((FieldInfo)dataMediator).FieldType;
        }

        public object? GetValue(MemberInfo dataMediator) {
            return ((FieldInfo)dataMediator).GetValue(Obj);
        }

        public object GetOrInitializeValue(MemberInfo dataMediator) {
            var value = ((FieldInfo)dataMediator).GetValue(Obj) ??
                   Activator.CreateInstance(((FieldInfo)dataMediator).FieldType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)dataMediator).FieldType)!)!;
            ((FieldInfo)dataMediator).SetValue(Obj, value);
            return value;
        }

        public void SetValue(MemberInfo dataMediator, object? value) {
            ((FieldInfo)dataMediator).SetValue(Obj, value);
        }

        public bool SetOrAddValue(MemberInfo dataMediator, object? value) {
            throw new NotImplementedException();
        }

        public override  string DataMemberToString(MemberInfo memberInfo) {
            throw new NotImplementedException();
        }

        public override IDictionary<TKey, MemberInfo> GetDataMembersByKeys() {
            throw new NotImplementedException();
        }

        public object GetOrInitializeValue(TKey key) {
            var dataMediator = GetDataMediator(key);
            return GetOrInitializeValue(dataMediator);
        }

        public Type GetType(TKey key) {
            var dataMediator = GetDataMediator(key);
            return GetType(dataMediator);
        }

        public object? GetValue(TKey key) {
            var dataMediator = GetDataMediator(key);
            return GetOrInitializeValue(dataMediator);
        }

        public override bool HasDataMember(MemberInfo memberInfo) {
            throw new NotImplementedException();
        }

        public bool SetOrAddValue(TKey key, object? value) {
            var dataMediator = GetDataMediator(key);
            return SetOrAddValue(dataMediator, value);
        }

        public void SetValue(TKey key, object? value) {
            var dataMediator = GetDataMediator(key);
            SetValue(dataMediator, value);
        }
    }
}
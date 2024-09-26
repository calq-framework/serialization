using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public abstract class FieldAccessorBase<TKey> : ClassMemberResolverBase<TKey, object?>, IDataAccessor<TKey, object?, MemberInfo> {
        public IEnumerable<MemberInfo> DataMediators => ParentType.GetFields();

        public FieldAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        public Type GetType(MemberInfo dataMediator) {
            return ((FieldInfo)dataMediator).FieldType;
        }

        public object? GetValue(MemberInfo dataMediator) {
            return ((FieldInfo)dataMediator).GetValue(ParentObject);
        }

        public object GetValueOrInitialize(MemberInfo dataMediator) {
            var value = ((FieldInfo)dataMediator).GetValue(ParentObject) ??
                   Activator.CreateInstance(((FieldInfo)dataMediator).FieldType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)dataMediator).FieldType)!)!;
            ((FieldInfo)dataMediator).SetValue(ParentObject, value);
            return value;
        }

        public void SetValue(MemberInfo dataMediator, object? value) {
            ((FieldInfo)dataMediator).SetValue(ParentObject, value);
        }

        public object GetValueOrInitialize(TKey key) {
            var dataMediator = GetDataMediator(key);
            return GetValueOrInitialize(dataMediator);
        }

        public Type GetType(TKey key) {
            var dataMediator = GetDataMediator(key);
            return GetType(dataMediator);
        }

        public object? GetValue(TKey key) {
            var dataMediator = GetDataMediator(key);
            return GetValueOrInitialize(dataMediator);
        }

        public void SetValue(TKey key, object? value) {
            var dataMediator = GetDataMediator(key);
            SetValue(dataMediator, value);
        }

        public string DataMediatorToString(MemberInfo dataMediator) {
            return dataMediator.Name;
        }
    }
}
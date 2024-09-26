using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public abstract class PropertyAccessorBase<TKey> : ClassMemberResolverBase<TKey, object?>, IDataAccessor<TKey, object?, MemberInfo> {
        public IEnumerable<MemberInfo> DataMediators => ParentType.GetProperties();

        public PropertyAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        public bool Contains(MemberInfo dataMediator) {
            return dataMediator is PropertyInfo;
        }

        public Type GetType(MemberInfo dataMediator) {
            return ((PropertyInfo)dataMediator).PropertyType;
        }

        public object? GetValue(MemberInfo dataMediator) {
            return ((PropertyInfo)dataMediator).GetValue(ParentObject);
        }

        public object GetOrInitializeValue(MemberInfo dataMediator) {
            var value = ((PropertyInfo)dataMediator).GetValue(ParentObject) ??
                   Activator.CreateInstance(((PropertyInfo)dataMediator).PropertyType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((PropertyInfo)dataMediator).PropertyType)!)!;
            ((PropertyInfo)dataMediator).SetValue(ParentObject, value);
            return value;
        }

        public void SetValue(MemberInfo dataMediator, object? value) {
            ((PropertyInfo)dataMediator).SetValue(ParentObject, value);
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

        public void SetValue(TKey key, object? value) {
            var dataMediator = GetDataMediator(key);
            SetValue(dataMediator, value);
        }

        public string DataMediatorToString(MemberInfo dataMediator) {
            return dataMediator.Name;
        }
    }
}
using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public abstract class PropertyAccessorBase<TKey> : ClassMemberResolverBase<TKey, object?>, IDataAccessor<TKey, object?, MemberInfo> {
        public IEnumerable<MemberInfo> DataMediators => ParentType.GetProperties();

        public object? this[MemberInfo dataMediator] {
            get {
                return ((PropertyInfo)dataMediator).GetValue(ParentObject);
            }
            set {
                ((PropertyInfo)dataMediator).SetValue(ParentObject, value);
            }
        }

        public object? this[TKey key] {
            get {
                var dataMediator = GetDataMediator(key);
                return this[dataMediator];
            }
            set {
                var dataMediator = GetDataMediator(key);
                this[dataMediator] = value;
            }
        }

        public PropertyAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        public Type GetDataType(MemberInfo dataMediator) {
            return ((PropertyInfo)dataMediator).PropertyType;
        }

        public object GetValueOrInitialize(MemberInfo dataMediator) {
            var value = ((PropertyInfo)dataMediator).GetValue(ParentObject) ??
                   Activator.CreateInstance(((PropertyInfo)dataMediator).PropertyType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((PropertyInfo)dataMediator).PropertyType)!)!;
            ((PropertyInfo)dataMediator).SetValue(ParentObject, value);
            return value;
        }

        public object GetValueOrInitialize(TKey key) {
            var dataMediator = GetDataMediator(key);
            return GetValueOrInitialize(dataMediator);
        }

        public Type GetDataType(TKey key) {
            var dataMediator = GetDataMediator(key);
            return GetDataType(dataMediator);
        }

        public string DataMediatorToString(MemberInfo dataMediator) {
            return dataMediator.Name;
        }
    }
}
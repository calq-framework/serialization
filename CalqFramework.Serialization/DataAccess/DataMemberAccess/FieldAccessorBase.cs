using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public abstract class FieldAccessorBase<TKey> : ClassMemberResolverBase<TKey, object?>, IDataAccessor<TKey, object?, MemberInfo> {
        public IEnumerable<MemberInfo> DataMediators => ParentType.GetFields();

        public virtual object? this[MemberInfo dataMediator] {
            get {
                return ((FieldInfo)dataMediator).GetValue(ParentObject);
            }
            set {
                ((FieldInfo)dataMediator).SetValue(ParentObject, value);
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

        public FieldAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr) {
        }

        public Type GetDataType(MemberInfo dataMediator) {
            return ((FieldInfo)dataMediator).FieldType;
        }

        public object GetValueOrInitialize(MemberInfo dataMediator) {
            var value = ((FieldInfo)dataMediator).GetValue(ParentObject) ??
                   Activator.CreateInstance(((FieldInfo)dataMediator).FieldType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)dataMediator).FieldType)!)!;
            ((FieldInfo)dataMediator).SetValue(ParentObject, value);
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
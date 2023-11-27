using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    public abstract class PropertyAccessorBase : DataMemberAccessorBase {
        public PropertyAccessorBase(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override Type GetDataMemberType(Type type, string dataMemberKey) {
            var dataMember = GetDataMemberOrThrow(type, dataMemberKey);

            return ((PropertyInfo)dataMember).PropertyType;
        }

        public override object? GetDataMemberValue(object obj, string dataMemberKey) {
            var dataMember = GetDataMemberOrThrow(obj.GetType(), dataMemberKey);

            return ((PropertyInfo)dataMember).GetValue(obj);
        }

        public override object GetOrInitializeDataMemberValue(object obj, string dataMemberKey) {
            var dataMember = GetDataMemberOrThrow(obj.GetType(), dataMemberKey);

            var value = ((PropertyInfo)dataMember).GetValue(obj) ??
                   Activator.CreateInstance(((PropertyInfo)dataMember).PropertyType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((PropertyInfo)dataMember).PropertyType)!)!;
            ((PropertyInfo)dataMember).SetValue(obj, value);
            return value;
        }

        public override void SetDataMemberValue(object obj, string dataMemberKey, object? value) {
            var dataMember = GetDataMemberOrThrow(obj.GetType(), dataMemberKey);

            ((PropertyInfo)dataMember).SetValue(obj, value);
        }
    }
}
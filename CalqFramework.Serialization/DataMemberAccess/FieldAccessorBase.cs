using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    public abstract class FieldAccessorBase : DataMemberAccessorBase {
        public FieldAccessorBase(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override Type GetDataMemberType(Type type, string dataMemberKey) {
            var dataMember = GetDataMemberOrThrow(type, dataMemberKey);

            return ((FieldInfo)dataMember).FieldType;
        }

        public override object? GetDataMemberValue(object obj, string dataMemberKey) {
            var dataMember = GetDataMemberOrThrow(obj.GetType(), dataMemberKey);

            return ((FieldInfo)dataMember).GetValue(obj);
        }

        public override object GetOrInitializeDataMemberValue(object obj, string dataMemberKey) {
            var dataMember = GetDataMemberOrThrow(obj.GetType(), dataMemberKey);

            var value = ((FieldInfo)dataMember).GetValue(obj) ??
                   Activator.CreateInstance(((FieldInfo)dataMember).FieldType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)dataMember).FieldType)!)!;
            ((FieldInfo)dataMember).SetValue(obj, value);
            return value;
        }

        public override void SetDataMemberValue(object obj, string dataMemberKey, object? value) {
            var dataMember = GetDataMemberOrThrow(obj.GetType(), dataMemberKey);

            ((FieldInfo)dataMember).SetValue(obj, value);
        }
    }
}
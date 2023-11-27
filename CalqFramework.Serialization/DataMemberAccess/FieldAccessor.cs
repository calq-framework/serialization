using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    internal class FieldAccessor : DataMemberAccessorBase {
        public FieldAccessor(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override MemberInfo GetDataMember(Type type, string dataMemberKey) {
            return (MemberInfo?)type.GetField(dataMemberKey, BindingAttr) ?? throw new MissingMemberException();
        }

        public override Type GetDataMemberType(Type type, string dataMemberKey) {
            var dataMember = GetDataMember(type, dataMemberKey);

            return ((FieldInfo)dataMember).FieldType;
        }

        public override object? GetDataMemberValue(object obj, string dataMemberKey) {
            var dataMember = GetDataMember(obj.GetType(), dataMemberKey);

            return ((FieldInfo)dataMember).GetValue(obj);
        }

        public override object GetOrInitializeDataMemberValue(object obj, string dataMemberKey) {
            var dataMember = GetDataMember(obj.GetType(), dataMemberKey);

            var value = ((FieldInfo)dataMember).GetValue(obj) ??
                   Activator.CreateInstance(((FieldInfo)dataMember).FieldType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)dataMember).FieldType)!)!;
            ((FieldInfo)dataMember).SetValue(obj, value);
            return value;
        }

        public override void SetDataMemberValue(object obj, string dataMemberKey, object? value) {
            var dataMember = GetDataMember(obj.GetType(), dataMemberKey);

            ((FieldInfo)dataMember).SetValue(obj, value);
        }
    }
}
using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    internal class FieldAndPropertyAccessor : DataMemberAccessorBase {
        public FieldAndPropertyAccessor(BindingFlags bindingAttr) : base(bindingAttr) {
        }

        public override MemberInfo GetDataMember(Type type, string dataMemberKey) {
            return (MemberInfo?)type.GetField(dataMemberKey, BindingAttr) ?? (MemberInfo?)type.GetProperty(dataMemberKey) ?? throw new MissingMemberException();
        }

        protected static bool IsField(MemberInfo memberInfo) {
            return memberInfo.MemberType == MemberTypes.Field;
        }

        public override Type GetDataMemberType(Type type, string dataMemberKey) {
            var dataMember = GetDataMember(type, dataMemberKey);

            if (IsField(dataMember)) {
                return ((FieldInfo)dataMember).FieldType;
            } else { // assume property
                return ((PropertyInfo)dataMember).PropertyType;
            }
        }

        public override object? GetDataMemberValue(object obj, string dataMemberKey) {
            var dataMember = GetDataMember(obj.GetType(), dataMemberKey);

            if (IsField(dataMember)) {
                return ((FieldInfo)dataMember).GetValue(obj);
            } else { // assume property
                return ((PropertyInfo)dataMember).GetValue(obj);
            }
        }

        public override object GetOrInitializeDataMemberValue(object obj, string dataMemberKey) {
            var dataMember = GetDataMember(obj.GetType(), dataMemberKey);

            if (IsField(dataMember)) {
                var value = ((FieldInfo)dataMember).GetValue(obj) ??
                    Activator.CreateInstance(((FieldInfo)dataMember).FieldType) ?? // might be null for nullables
                    Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)dataMember).FieldType)!)!; // assume nullable
                ((FieldInfo)dataMember).SetValue(obj, value);
                return value;
            } else { // assume property
                var value = ((PropertyInfo)dataMember).GetValue(obj) ??
                    Activator.CreateInstance(((PropertyInfo)dataMember).PropertyType) ??
                    Activator.CreateInstance(Nullable.GetUnderlyingType(((PropertyInfo)dataMember).PropertyType)!)!;
                ((PropertyInfo)dataMember).SetValue(obj, value);
                return value;
            }
        }

        public override void SetDataMemberValue(object obj, string dataMemberKey, object? value) {
            var dataMember = GetDataMember(obj.GetType(), dataMemberKey);

            if (IsField(dataMember)) {
                ((FieldInfo)dataMember).SetValue(obj, value);
            } else { // assume property
                ((PropertyInfo)dataMember).SetValue(obj, value);
            }
        }
    }
}
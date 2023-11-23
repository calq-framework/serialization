using Newtonsoft.Json.Linq;
using System.Collections;
using System.Reflection;

namespace CalqFramework.Serialization;
public abstract class DataMemberAccessorBase {

    public abstract MemberInfo GetDataMember(Type type, string dataMemberKey);

    private static bool IsField(MemberInfo memberInfo) {
        return memberInfo.MemberType == MemberTypes.Field;
    }

    //private static bool IsNullable(Type type) {
    //    return Nullable.GetUnderlyingType(type) != null;
    //}

    public Type GetDataMemberType(Type type, string dataMemberKey) {
        var dataMember = GetDataMember(type, dataMemberKey);

        if (IsField(dataMember)) {
            return ((FieldInfo)dataMember).FieldType;
        } else { // assume property
            return ((PropertyInfo)dataMember).PropertyType;
        }
    }

    public object? GetDataMemberValue(object obj, string dataMemberKey) {
        var dataMember = GetDataMember(obj.GetType(), dataMemberKey);

        if (IsField(dataMember)) {
            return ((FieldInfo)dataMember).GetValue(obj);
        } else { // assume property
            return ((PropertyInfo)dataMember).GetValue(obj);
        }
    }

    public object GetOrInitializeDataMemberValue(object obj, string dataMemberKey) {
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

    public void SetDataMemberValue(object obj, string dataMemberKey, object? value) {
        var dataMember = GetDataMember(obj.GetType(), dataMemberKey);

        if (IsField(dataMember)) {
            ((FieldInfo)dataMember).SetValue(obj, value);
        } else { // assume property
            ((PropertyInfo)dataMember).SetValue(obj, value);
        }
    }

    public object InitializeDataMemberValue(object obj, string dataMemberKey) {
        var dataMember = GetDataMember(obj.GetType(), dataMemberKey);

        if (IsField(dataMember)) {
            var value = Activator.CreateInstance(((FieldInfo)dataMember).FieldType) ?? // might be null for nullables
                Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)dataMember).FieldType)!)!; // assume nullable
            ((FieldInfo)dataMember).SetValue(obj, value);
            return value;
        } else { // assume property
            var value = Activator.CreateInstance(((PropertyInfo)dataMember).PropertyType) ??
                Activator.CreateInstance(Nullable.GetUnderlyingType(((PropertyInfo)dataMember).PropertyType)!)!;
            ((PropertyInfo)dataMember).SetValue(obj, value);
            return value;
        }
    }

    public bool IsPrimitive(ICollection collection) {
        return IsPrimitive(collection.GetType().GetGenericArguments()[0]);
    }

    public bool IsPrimitive(object obj, string fieldOrPropertyName) {
        return IsPrimitive(GetDataMemberType(obj.GetType(), fieldOrPropertyName));
    }

    public bool IsPrimitive(Type type) {
        if (type.IsPrimitive || type == typeof(Decimal) || type == typeof(String)) {
            return true;
        }
        return false;
    }
}

using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    public abstract class FieldAccessorBase : DataMemberAccessorBase
    {
        public FieldAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr)
        {
        }

        public override Type GetType(string dataMemberKey)
        {
            var dataMember = GetDataMemberOrThrow(dataMemberKey);

            return ((FieldInfo)dataMember).FieldType;
        }

        public override object? GetValue(string dataMemberKey)
        {
            var dataMember = GetDataMemberOrThrow(dataMemberKey);

            return ((FieldInfo)dataMember).GetValue(Obj);
        }

        public override object GetOrInitializeValue(string dataMemberKey)
        {
            var dataMember = GetDataMemberOrThrow(dataMemberKey);

            var value = ((FieldInfo)dataMember).GetValue(Obj) ??
                   Activator.CreateInstance(((FieldInfo)dataMember).FieldType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)dataMember).FieldType)!)!;
            ((FieldInfo)dataMember).SetValue(Obj, value);
            return value;
        }

        public override void SetValue(string dataMemberKey, object? value)
        {
            var dataMember = GetDataMemberOrThrow(dataMemberKey);

            ((FieldInfo)dataMember).SetValue(Obj, value);
        }
    }
}
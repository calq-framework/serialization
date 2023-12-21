using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    public abstract class PropertyAccessorBase : DataMemberAccessorBase
    {
        public PropertyAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr)
        {
        }

        public override Type GetType(string dataMemberKey)
        {
            var dataMember = GetDataMemberOrThrow(dataMemberKey);

            return ((PropertyInfo)dataMember).PropertyType;
        }

        public override object? GetValue(string dataMemberKey)
        {
            var dataMember = GetDataMemberOrThrow(dataMemberKey);

            return ((PropertyInfo)dataMember).GetValue(Obj);
        }

        public override object GetOrInitializeValue(string dataMemberKey)
        {
            var dataMember = GetDataMemberOrThrow(dataMemberKey);

            var value = ((PropertyInfo)dataMember).GetValue(Obj) ??
                   Activator.CreateInstance(((PropertyInfo)dataMember).PropertyType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((PropertyInfo)dataMember).PropertyType)!)!;
            ((PropertyInfo)dataMember).SetValue(Obj, value);
            return value;
        }

        public override void SetValue(string dataMemberKey, object? value)
        {
            var dataMember = GetDataMemberOrThrow(dataMemberKey);

            ((PropertyInfo)dataMember).SetValue(Obj, value);
        }
    }
}
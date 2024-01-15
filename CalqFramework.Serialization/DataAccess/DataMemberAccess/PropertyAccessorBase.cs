using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    public abstract class PropertyAccessorBase : DataMemberAccessorBase
    {
        public PropertyAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr)
        {
        }

        public override Type GetType(string key)
        {
            var dataMember = GetDataMemberOrThrow(key);

            return ((PropertyInfo)dataMember).PropertyType;
        }

        public override object? GetValue(string key)
        {
            var dataMember = GetDataMemberOrThrow(key);

            return ((PropertyInfo)dataMember).GetValue(Obj);
        }

        public override object GetOrInitializeValue(string key)
        {
            var dataMember = GetDataMemberOrThrow(key);

            var value = ((PropertyInfo)dataMember).GetValue(Obj) ??
                   Activator.CreateInstance(((PropertyInfo)dataMember).PropertyType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((PropertyInfo)dataMember).PropertyType)!)!;
            ((PropertyInfo)dataMember).SetValue(Obj, value);
            return value;
        }

        public override void SetValue(string key, object? value)
        {
            var dataMember = GetDataMemberOrThrow(key);

            ((PropertyInfo)dataMember).SetValue(Obj, value);
        }
    }
}
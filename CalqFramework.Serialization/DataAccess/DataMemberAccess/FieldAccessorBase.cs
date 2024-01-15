using System.Reflection;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess
{
    public abstract class FieldAccessorBase : DataMemberAccessorBase
    {
        public FieldAccessorBase(object obj, BindingFlags bindingAttr) : base(obj, bindingAttr)
        {
        }

        public override Type GetType(string key)
        {
            var dataMember = GetDataMember(key);

            return ((FieldInfo)dataMember).FieldType;
        }

        public override object? GetValue(string key)
        {
            var dataMember = GetDataMember(key);

            return ((FieldInfo)dataMember).GetValue(Obj);
        }

        public override object GetOrInitializeValue(string key)
        {
            var dataMember = GetDataMember(key);

            var value = ((FieldInfo)dataMember).GetValue(Obj) ??
                   Activator.CreateInstance(((FieldInfo)dataMember).FieldType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(((FieldInfo)dataMember).FieldType)!)!;
            ((FieldInfo)dataMember).SetValue(Obj, value);
            return value;
        }

        public override void SetValue(string key, object? value)
        {
            var dataMember = GetDataMember(key);

            ((FieldInfo)dataMember).SetValue(Obj, value);
        }
    }
}
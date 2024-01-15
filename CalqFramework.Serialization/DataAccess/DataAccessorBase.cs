using System.Collections;

namespace CalqFramework.Serialization.DataAccess
{
    public abstract class DataAccessorBase : IDataAccessor
    {
        public abstract object GetOrInitializeValue(string key);
        public abstract Type GetType(string key);
        public abstract object? GetValue(string key);
        public abstract bool HasKey(string key);

        public void SetOrAddValue(string key, object? value)
        {
            var obj = GetValue(key);
            if (obj is not ICollection collectionObj)
            {
                SetValue(key, value);
            }
            else
            {
                CollectionAccessor.AddValue(collectionObj, value);
            }
        }

        public abstract void SetValue(string key, object? value);
    }
}

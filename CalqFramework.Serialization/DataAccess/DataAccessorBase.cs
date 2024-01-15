using System.Collections;

namespace CalqFramework.Serialization.DataAccess
{
    public abstract class DataAccessorBase : IDataAccessor
    {
        public abstract object GetOrInitializeValue(string key);
        public abstract Type GetType(string key);
        public abstract object? GetValue(string key);
        public abstract bool HasKey(string key);

        public virtual bool SetOrAddValue(string key, object? value)
        {
            var obj = GetValue(key);
            if (obj is not ICollection collectionObj)
            {
                SetValue(key, value);
                return false;
            }
            else
            {
                AddValue(collectionObj, value);
                return true;
            }
        }

        protected virtual void AddValue(ICollection collectionObj, object? value) {
            CollectionAccessor.AddValue(collectionObj, value);
        }

        public abstract void SetValue(string key, object? value);
    }
}

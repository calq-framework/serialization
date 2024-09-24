using System.Collections;

namespace CalqFramework.Serialization.DataAccess
{
    public abstract class DataAccessorBase<TKey, TValue> : IDataAccessor<TKey, TValue>
    {
        public abstract object GetOrInitializeValue(TKey key);
        public abstract Type GetType(TKey key);
        public abstract object? GetValue(TKey key);
        public abstract bool Contains(TKey key);

        public virtual bool SetOrAddValue(TKey key, TValue? value)
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

        protected virtual void AddValue(ICollection collectionObj, TValue? value) {
            CollectionAccessor.AddValue(collectionObj, value);
        }

        public abstract void SetValue(TKey key, TValue? value);
    }
}

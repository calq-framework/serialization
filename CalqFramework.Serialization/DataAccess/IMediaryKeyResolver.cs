using System.Reflection;

namespace CalqFramework.Serialization.DataAccess {
    public interface IMediaryKeyResolver<TKey, TValue> {
        bool TryGetMediaryKey(TKey key, out TValue result);
        TValue GetMediaryKey(TKey key);
    }
}
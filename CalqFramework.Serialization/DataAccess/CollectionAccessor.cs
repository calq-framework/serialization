using System.Collections;
using CalqFramework.Serialization.Text;

namespace CalqFramework.Serialization.DataAccess;
public static class CollectionAccessor
{
    public static object? GetValue(ICollection collection, string key)
    {
        return collection switch
        {
            Array array => array.GetValue(int.Parse(key)),
            IList list => list[int.Parse(key)],
            IDictionary dictionary => dictionary[ValueParser.ParseValue(key, dictionary.GetType().GetGenericArguments()[0])],
            _ => throw new Exception("unsupported collection")
        };
    }

    public static object? GetOrInitializeValue(ICollection collection, string key)
    {
        switch (collection)
        {
            case Array array:
                var arrayElement = array.GetValue(int.Parse(key));
                if (arrayElement == null)
                {
                    arrayElement = Activator.CreateInstance(collection.GetType().GetElementType()!) ??
                        Activator.CreateInstance(Nullable.GetUnderlyingType(collection.GetType().GetGenericArguments()[0])!)!;
                    array.SetValue(arrayElement, int.Parse(key));
                }
                return arrayElement;
            case IList list:
                var listElement = list[int.Parse(key)];
                if (listElement == null)
                {
                    listElement = Activator.CreateInstance(collection.GetType().GetGenericArguments()[0]!) ??
                        Activator.CreateInstance(Nullable.GetUnderlyingType(collection.GetType().GetGenericArguments()[0])!)!;
                    list[int.Parse(key)] = listElement;
                }
                return listElement;
            case IDictionary dictionary:
                var dictionaryElement = dictionary[ValueParser.ParseValue(key, dictionary.GetType().GetGenericArguments()[0])];
                if (dictionaryElement == null)
                {
                    dictionaryElement = Activator.CreateInstance(collection.GetType().GetGenericArguments()[1]!) ??
                        Activator.CreateInstance(Nullable.GetUnderlyingType(collection.GetType().GetGenericArguments()[0])!)!;
                    dictionary[ValueParser.ParseValue(key, dictionary.GetType().GetGenericArguments()[0])] = dictionaryElement;
                }
                return dictionaryElement;
            default:
                throw new Exception("unsupported collection");
        }
    }

    public static void SetValue(ICollection collection, string key, object? value)
    {
        switch (collection)
        {
            case Array array:
                array.SetValue(value, int.Parse(key));
                break;
            case IList list:
                list[int.Parse(key)] = value;
                break;
            case IDictionary dictionary:
                dictionary[ValueParser.ParseValue(key, dictionary.GetType().GetGenericArguments()[0])] = value;
                break;
            default:
                throw new Exception("unsupported collection");
        }
    }

    public static void AddValue(ICollection collection, object? value)
    {
        switch (collection)
        {
            case IList list:
                list.Add(value);
                break;
            default:
                throw new Exception("unsupported collection");
        }
    }

    public static object AddValue(ICollection collection) {
        switch (collection) {
            case IList list:
                var value = Activator.CreateInstance(collection.GetType().GetGenericArguments()[0]) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(collection.GetType().GetGenericArguments()[0])!)!;
                list.Add(value);
                return value;
            default:
                throw new Exception("unsupported collection");
        }
    }

    public static void RemoveValue(ICollection collection, string key)
    {
        switch (collection)
        {
            case IList list:
                list.RemoveAt(int.Parse(key));
                break;
            case IDictionary dictionary:
                dictionary.Remove(key);
                break;
            default:
                throw new Exception("unsupported collection");
        }
    }
}

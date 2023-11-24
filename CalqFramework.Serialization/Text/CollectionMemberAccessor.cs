using System.Collections;

namespace CalqFramework.Serialization.Text;
internal static class CollectionMemberAccessor
{
    public static object? GetChildValue(ICollection collection, string key)
    {
        return collection switch
        {
            Array array => array.GetValue(int.Parse(key)),
            IList list => list[int.Parse(key)],
            IDictionary dictionary => dictionary[ValueParser.ParseValue(key, dictionary.GetType().GetGenericArguments()[0])],
            _ => throw new Exception("unsupported collection")
        };
    }

    public static object? GetOrInitializeChildValue(ICollection collection, string key)
    {
        switch (collection)
        {
            case Array array:
                var arrayMemberValue = array.GetValue(int.Parse(key));
                if (arrayMemberValue == null)
                {
                    arrayMemberValue = Activator.CreateInstance(array.GetType().GetElementType()!);
                    array.SetValue(arrayMemberValue, int.Parse(key));
                }
                return arrayMemberValue;
            case IList list:
                var listMemberValue = list[int.Parse(key)];
                if (listMemberValue == null)
                {
                    listMemberValue = Activator.CreateInstance(list.GetType().GetGenericArguments()[0]!);
                    list[int.Parse(key)] = listMemberValue;
                }
                return listMemberValue;
            case IDictionary dictionary:
                var dictionaryMemberValue = dictionary[ValueParser.ParseValue(key, dictionary.GetType().GetGenericArguments()[0])];
                if (dictionaryMemberValue == null)
                {
                    dictionaryMemberValue = Activator.CreateInstance(dictionary.GetType().GetGenericArguments()[1]!);
                    dictionary[ValueParser.ParseValue(key, dictionary.GetType().GetGenericArguments()[0])] = dictionaryMemberValue;
                }
                return dictionaryMemberValue;
            default:
                throw new Exception("unsupported collection");
        }
    }

    public static void SetChildValue(ICollection collection, string key, object? value)
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

    public static void AddChildValue(ICollection collection, object? value)
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

    public static void DeleteChildValue(ICollection collection, string key)
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

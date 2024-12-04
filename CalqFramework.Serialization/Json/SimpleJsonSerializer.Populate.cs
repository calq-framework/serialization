using System.Collections;
using System.Text.Json;
using CalqFramework.Serialization.DataAccess;

namespace CalqFramework.Serialization.Json
{
    public partial class SimpleJsonSerializer : ISerializer
    {
        public void Populate<T>(ReadOnlySpan<byte> data, T obj)
        {
            var reader = new Utf8JsonReader(data, new JsonReaderOptions
            {
                CommentHandling = JsonCommentHandling.Skip
            });

            if (obj == null)
            {
                throw new ArgumentException("instance can't be null");
            }

            object? currentInstance = obj;
            var instanceStack = new Stack<object>();

            void ReadObject(ref Utf8JsonReader reader)
            {
                var classMembers = ClassDataMemberStoreFactory.CreateDataMemberStore(currentInstance);
                CollectionStore? collection = null!;
                if (currentInstance is ICollection) {
                    collection = new CollectionStore((ICollection)currentInstance);
                }

                while (true)
                {
                    reader.Read();
                    string propertyName;
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.PropertyName:
                            propertyName = reader.GetString()!;
                            break;
                        case JsonTokenType.EndObject:
                            if (instanceStack.Count == 0)
                            {
                                if (reader.Read())
                                {
                                    throw new JsonException();
                                }
                                return;
                            }
                            currentInstance = instanceStack.Pop();
                            return;
                        default:
                            throw new JsonException();
                    }

                    reader.Read();
                    object? value;
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.False:
                        case JsonTokenType.True:
                            value = reader.GetBoolean();
                            break;
                        case JsonTokenType.String:
                            value = reader.GetString();
                            break;
                        case JsonTokenType.Number:
                            value = reader.GetInt32();
                            break;
                        case JsonTokenType.Null:
                            value = null;
                            break;
                        case JsonTokenType.StartObject:
                            instanceStack.Push(currentInstance!);
                            if (currentInstance is not ICollection)
                            {
                                currentInstance = classMembers.GetValueOrInitialize(propertyName);
                            }
                            else
                            {
                                currentInstance = collection.GetValueOrInitialize(propertyName);
                            }
                            ReadObject(ref reader);
                            continue;
                        case JsonTokenType.StartArray:
                            instanceStack.Push(currentInstance!);
                            if (currentInstance is not ICollection)
                            {
                                currentInstance = classMembers.GetValueOrInitialize(propertyName);
                            }
                            else
                            {
                                currentInstance = collection.GetValueOrInitialize(propertyName);
                            }
                            ReadArray(ref reader);
                            continue;
                        default:
                            throw new JsonException();
                    }
                    if (currentInstance is not ICollection)
                    {
                        classMembers[propertyName] = value;
                    }
                    else
                    {
                        collection[propertyName] = value;
                    }
                }
            }

            void ReadArray(ref Utf8JsonReader reader)
            {
                var collection = new CollectionStore((ICollection)currentInstance!);
                while (true)
                {
                    reader.Read();
                    object? value;
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.False:
                        case JsonTokenType.True:
                            value = reader.GetBoolean();
                            break;
                        case JsonTokenType.String:
                            value = reader.GetString();
                            break;
                        case JsonTokenType.Number:
                            value = reader.GetInt32();
                            break;
                        case JsonTokenType.Null:
                            value = null;
                            break;
                        case JsonTokenType.StartObject:
                            instanceStack.Push(currentInstance);
                            currentInstance = collection.AddValue();
                            ReadObject(ref reader);
                            continue;
                        case JsonTokenType.StartArray:
                            instanceStack.Push(currentInstance);
                            currentInstance = collection.AddValue();
                            ReadArray(ref reader);
                            continue;
                        case JsonTokenType.EndArray:
                            if (instanceStack.Count == 0)
                            {
                                if (reader.Read())
                                {
                                    throw new JsonException();
                                }
                                return;
                            }
                            currentInstance = instanceStack.Pop();
                            return;
                        default:
                            throw new JsonException();
                    }
                    collection.AddValue(value);
                }
            }

            reader.Read();
            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    ReadObject(ref reader);
                    break;
                case JsonTokenType.StartArray:
                    ReadArray(ref reader);
                    break;
                default:
                    throw new JsonException();
            }
        }
    }
}

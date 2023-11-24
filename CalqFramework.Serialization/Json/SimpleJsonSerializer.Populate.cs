using System.Collections;
using System.Text.Json;
using CalqFramework.Serialization.DataMemberAccess;
using CalqFramework.Serialization.Text;

namespace CalqFramework.Serialization.Json {
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
                            if (currentInstance is not ICollection)
                            {
                                continue;
                            }
                            else
                            {
                                return;
                            }
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
                            instanceStack.Push(currentInstance);
                            if (currentInstance is not ICollection)
                            {
                                currentInstance = DataMemberAccessor.GetOrInitializeDataMemberValue(currentInstance, propertyName);
                            }
                            else
                            {
                                currentInstance = CollectionMemberAccessor.GetOrInitializeChildValue((ICollection)currentInstance, propertyName);
                            }
                            if (currentInstance == null)
                            {
                                throw new JsonException();
                            }
                            continue;
                        case JsonTokenType.StartArray:
                            instanceStack.Push(currentInstance);
                            value = DataMemberAccessor.GetOrInitializeDataMemberValue(currentInstance, propertyName);
                            if (currentInstance is not ICollection)
                            {
                                DataMemberAccessor.SetDataMemberValue(currentInstance, propertyName, value);
                            }
                            else
                            {
                                CollectionMemberAccessor.SetChildValue((ICollection)currentInstance, propertyName, value);
                            }
                            currentInstance = value;
                            if (currentInstance == null)
                            {
                                throw new JsonException();
                            }
                            ReadArray(ref reader);
                            continue;
                        default:
                            throw new JsonException();
                    }
                    if (currentInstance is not ICollection)
                    {
                        DataMemberAccessor.SetDataMemberValue(currentInstance, propertyName, value);
                    }
                    else
                    {
                        CollectionMemberAccessor.SetChildValue((ICollection)currentInstance, propertyName, value);
                    }
                }
            }

            void ReadArray(ref Utf8JsonReader reader)
            {
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
                            value = Activator.CreateInstance(currentInstance.GetType().GetGenericArguments()[0]);
                            CollectionMemberAccessor.AddChildValue((ICollection)currentInstance, value);
                            currentInstance = value;
                            if (currentInstance == null)
                            {
                                throw new JsonException();
                            }
                            ReadObject(ref reader);
                            continue;
                        case JsonTokenType.StartArray:
                            instanceStack.Push(currentInstance);
                            value = Activator.CreateInstance(currentInstance.GetType().GetGenericArguments()[0]);
                            CollectionMemberAccessor.AddChildValue((ICollection)currentInstance, value);
                            currentInstance = value;
                            if (currentInstance == null)
                            {
                                throw new JsonException();
                            }
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
                            if (currentInstance is not ICollection)
                            {
                                return;
                            }
                            else
                            {
                                continue;
                            }
                        default:
                            throw new JsonException();
                    }
                    CollectionMemberAccessor.AddChildValue((ICollection)currentInstance, value);
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

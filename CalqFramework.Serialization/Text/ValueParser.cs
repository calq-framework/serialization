﻿namespace CalqFramework.Serialization.Text;
public static class ValueParser
{
    public static bool IsParseable(Type type)
    {
        return type.IsPrimitive || type == typeof(string);
    }

    public static T ParseValue<T>(string value)
    {
        return (T)ParseValue(value, typeof(T));
    }

    public static object ParseValue(string value, Type targetType)
    {
        if (Nullable.GetUnderlyingType(targetType) != null)
        {
            targetType = Nullable.GetUnderlyingType(targetType)!;
        }

        object objValue;
        try
        {
            objValue = Type.GetTypeCode(targetType) switch
            {
                TypeCode.Boolean => bool.Parse(value),
                TypeCode.Byte => byte.Parse(value),
                TypeCode.SByte => sbyte.Parse(value),
                TypeCode.Char => char.Parse(value),
                TypeCode.Decimal => decimal.Parse(value),
                TypeCode.Double => double.Parse(value),
                TypeCode.Single => float.Parse(value),
                TypeCode.Int32 => int.Parse(value),
                TypeCode.UInt32 => uint.Parse(value),
                TypeCode.Int64 => long.Parse(value),
                TypeCode.UInt64 => ulong.Parse(value),
                TypeCode.Int16 => short.Parse(value),
                TypeCode.UInt16 => ushort.Parse(value),
                TypeCode.String => value,
                _ => throw new ArgumentException($"type cannot be parsed: {targetType.Name}"),
            };
        }
        catch (OverflowException ex)
        {
            long min;
            ulong max;
            switch (Type.GetTypeCode(targetType))
            {
                case TypeCode.Byte:
                    min = byte.MinValue;
                    max = byte.MaxValue;
                    break;
                case TypeCode.SByte:
                    min = sbyte.MinValue;
                    max = (ulong)sbyte.MaxValue;
                    break;
                case TypeCode.Char:
                    min = char.MinValue;
                    max = char.MaxValue;
                    break;
                case TypeCode.Int32:
                    min = int.MinValue;
                    max = int.MaxValue;
                    break;
                case TypeCode.UInt32:
                    min = uint.MinValue;
                    max = uint.MaxValue;
                    break;
                case TypeCode.Int64:
                    min = long.MinValue;
                    max = long.MaxValue;
                    break;
                case TypeCode.UInt64:
                    min = (long)ulong.MinValue;
                    max = ulong.MaxValue;
                    break;
                case TypeCode.Int16:
                    min = short.MinValue;
                    max = (ulong)short.MaxValue;
                    break;
                case TypeCode.UInt16:
                    min = ushort.MinValue;
                    max = ushort.MaxValue;
                    break;
                default:
                    throw;
            }
            throw new OverflowException($"{value} ({min}-{max})", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException($"value type mismatch: {value} is not {targetType.Name}", ex);
        }
        return objValue;
    }
}

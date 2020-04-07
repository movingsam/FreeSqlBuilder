using System;
using System.Collections.Generic;

namespace FreeSqlBuilder.Core.Utilities
{
    public static class IDictionaryExtensions
    {
        public static bool Value<TKey, TValue, TTypedValue>(this IDictionary<TKey, TValue> dic, TKey key, out TTypedValue value)
        {
            value = default;
            if (dic == null) { return false; }
            if (!dic.TryGetValue(key, out TValue val))
            {
                return false;
            }

            object objVal = val;
            var valType = typeof(TValue);
            var typedValType = typeof(TTypedValue);
            if (typedValType.IsEnum && objVal is string)
            {
                value = (TTypedValue)System.Enum.Parse(typedValType, objVal.ToString());
            }
            else if (typeof(IConvertible).IsAssignableFrom(typedValType))
            {
                value = (TTypedValue)Convert.ChangeType(objVal, typedValType);
            }
            else
            {
                value = (TTypedValue)objVal;
            }
            return true;
        }
        public static void EnsureValue<TKey, TValue, TTypedValue>(this IDictionary<TKey, TValue> dic, TKey key, out TTypedValue value)
        {
            if (!dic.Value(key, out value))
            {
                throw new Exception($"Can not find Parameter:{key}!");
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.Globalization;

namespace Sql_Auto_Data_Discovery.Business.Extentions
{
    public static class StringExtensions
    {
        public static string ToChar(this bool value)
        {
            return value ? "T" : "F";
        }

        public static string Filter(this string text)
        {
            return !text.IsNullOrWhiteSpace()
                ? text
                    .Trim()
                    .Replace("'", string.Empty)
                : string.Empty;
        }

        public static bool IsSet(this string value)
        {
            return !value.IsNullOrWhiteSpace();
        }

        public static bool IsNotSet(this string value)
        {
            return value.IsNullOrWhiteSpace();
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null)
                return true;
            for (int index = 0; index < value.Length; ++index)
            {
                if (!char.IsWhiteSpace(value[index]))
                    return false;
            }
            return true;
        }

        public static bool IsEmpty(this string value)
        {
            return value.IsNullOrWhiteSpace();
        }

        public static int AsInt(this string value)
        {
            return AsInt(value, 0);
        }

        public static int AsInt(this string value, int defaultValue)
        {
            int result;
            if (!int.TryParse(value, out result))
                return defaultValue;
            else
                return result;
        }

        public static Decimal AsDecimal(this string value)
        {
            return As<Decimal>(value);
        }

        public static Decimal AsDecimal(this string value, Decimal defaultValue)
        {
            return As(value, defaultValue);
        }

        public static float AsFloat(this string value)
        {
            return AsFloat(value, 0.0f);
        }

        public static float AsFloat(this string value, float defaultValue)
        {
            float result;
            if (!float.TryParse(value, out result))
                return defaultValue;
            else
                return result;
        }

        public static DateTime AsDateTime(this string value)
        {
            return AsDateTime(value, new DateTime());
        }

        public static DateTime AsDateTime(this string value, DateTime defaultValue)
        {
            DateTime result;
            if (!DateTime.TryParse(value, out result))
                return defaultValue;
            else
                return result;
        }

        public static TValue As<TValue>(this string value)
        {
            return As(value, default(TValue));
        }

        public static bool AsBoolChar(this string value)
        {
            return value.IsSet() && value.ToUpper().Equals("T");
        }

        public static bool AsBool(this string value)
        {
            return AsBool(value, false);
        }

        public static bool AsBool(this string value, bool defaultValue)
        {
            bool result;
            if (!bool.TryParse(value, out result))
                return defaultValue;
            else
                return result;
        }

        public static TValue As<TValue>(this string value, TValue defaultValue)
        {
            try
            {
                TypeConverter converter1 = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter1.CanConvertFrom(typeof(string)))
                    return (TValue)converter1.ConvertFrom((object)value);
                TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(string));
                if (converter2.CanConvertTo(typeof(TValue)))
                    return (TValue)converter2.ConvertTo((object)value, typeof(TValue));
            }
            catch
            {
            }
            return defaultValue;
        }

        public static bool IsBool(this string value)
        {
            bool result;
            return bool.TryParse(value, out result);
        }

        public static bool IsInt(this string value)
        {
            int result;
            return int.TryParse(value, out result);
        }

        public static bool IsDecimal(this string value)
        {
            return Is<Decimal>(value);
        }

        public static bool IsFloat(this string value)
        {
            float result;
            return float.TryParse(value, out result);
        }

        public static bool IsDateTime(this string value)
        {
            DateTime result;
            return DateTime.TryParse(value, out result);
        }

        public static bool Is<TValue>(this string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
            if (converter != null)
            {
                try
                {
                    if (value != null)
                    {
                        if (!converter.CanConvertFrom((ITypeDescriptorContext)null, value.GetType()))
                            goto label_5;
                    }
                    converter.ConvertFrom((ITypeDescriptorContext)null, CultureInfo.CurrentCulture, (object)value);
                    return true;
                }
                catch
                {
                }
            }
        label_5:
            return false;
        }
    }
}
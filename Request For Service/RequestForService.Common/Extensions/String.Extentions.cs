using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace RequestForService.Common.Extensions
{
	public static class StringExtentions
	{
		public static string Join(this IEnumerable<string> source)
		{
			return string.Join(Environment.NewLine, source);
		}

		public static string Join(this IEnumerable<string> source, string separator)
		{
			return string.Join(separator, source);
		}

		public static string RemoveSpaces(this string source)
		{
			return source.Trim().Replace(@" ", "");
		}

		public static string RemoveBlanks(this string source)
		{
			StringBuilder sb = new StringBuilder(source.Length);
			for (int i = 0; i < source.Length; i++)
			{
				char c = source[i];
				switch (c)
				{
					case '\r':
					case '\n':
					case '\t':
					case ' ':
						continue;
					default:
						sb.Append(c);
						break;
				}
			}
			return sb.ToString();
		}

		public static T To<T>(this string source) 
		{
			Type type = typeof(T);
			Type underlyingType = Nullable.GetUnderlyingType(type);
			bool isEmpty = string.IsNullOrWhiteSpace(source);
			bool hasUnderlyingType = underlyingType != null;
			bool isEnum = hasUnderlyingType ? underlyingType.IsEnum : type.IsEnum;
			if (isEmpty)
			{
				if (hasUnderlyingType)
				{
					return default(T); 
				}
				else
				{
					throw new InvalidCastException(string.Format(
						"Can not cast to type:{0}. This is not a nullable type.", type.Name));
				}
			}
			try
			{
				if (isEnum)
				{
					return hasUnderlyingType
						? (T) Enum.Parse(underlyingType, source)
						: (T) Enum.Parse(type, source);
				}
				else
				{
					return hasUnderlyingType
						? (T) Convert.ChangeType(source, underlyingType)
						: (T) Convert.ChangeType(source, type);
				}
			}
			catch
			{
				if (hasUnderlyingType)
				{
					return default(T);
				}
				else
				{
					throw;
				}
			}
		}

		public static bool TryTo<T>(this string source, out T value)
		{
			Type type = typeof(T);
			Type underlyingType = Nullable.GetUnderlyingType(type);
			bool isEmpty = string.IsNullOrWhiteSpace(source);
			bool hasUnderlyingType = underlyingType != null;
			bool isEnum = hasUnderlyingType ? underlyingType.IsEnum : type.IsEnum;
			if (isEmpty)
			{
				if (hasUnderlyingType)
				{
					value = default(T);
					return true;
				}
				else
				{
					value = default(T);
					return false;
				}
			}
			try
			{
				if (isEnum)
				{
					value = hasUnderlyingType
						? (T) Enum.Parse(underlyingType, source)
						: (T) Enum.Parse(type, source);
				}
				else
				{
					value = hasUnderlyingType 
						? (T) Convert.ChangeType(source, underlyingType) 
						: (T) Convert.ChangeType(source, type);
				}
				return true;
			}
			catch
			{
				value = default(T);
				return false;
			}
		}

        public static string Filter(this string text)
        {
            return !IsNullOrWhiteSpace(text)
                ? text
                    .Trim()
                    .Replace("'", string.Empty)
                : string.Empty;
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
            return string.IsNullOrEmpty(value);
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
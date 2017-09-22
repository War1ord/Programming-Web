using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace BudgetManager.Common.Helpers
{
    public class ObjectHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            return GetMemberInfo(expression).Name;
        }

        public static string GetDescription<T>(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static Type GetMemberType<T>(Expression<Func<T>> expression)
        {
            return GetMemberExpression(expression).Type;
        }

        public static MemberInfo GetMemberInfo<T>(Expression<Func<T>> expression)
        {
            return GetMemberExpression(expression).Member;
        }

        public static IEnumerable<PropertyInfo> FindProperties<T>(Expression<Func<T>> expression)
        {
            return GetMemberExpression(expression).Type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        private static MemberExpression GetMemberExpression<T>(Expression<Func<T>> expression)
        {
            return (MemberExpression)expression.Body;
        }

        public static object GetMemberInstance<T>(Expression<Func<T>> expression)
        {
            var constantExpression = (GetMemberExpression(expression).Expression as ConstantExpression);
            return constantExpression != null ? constantExpression.Value : null;
        }

        public static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }

    }
}
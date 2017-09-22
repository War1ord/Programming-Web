using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using NHibernate.Linq;

namespace BudgetManager.Extentions
{
	/// <summary>
	/// The Object Extentions Method Collection
	/// </summary>
	public static class ObjectExtentions
	{
		/// <summary>
		/// Converts the objects properties To a object array.
		/// </summary>
		/// <param name="Object">The object.</param>
		/// <param name="exludedProperties">The Properties to exclude. (Optional)</param>
		/// <returns></returns>
		public static object[] ToObjectArray(this object Object, params string[] exludedProperties)
		{
			//get properties
			const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
			var properties = Object.GetType().GetProperties(bindingFlags);
			//remove excluded Properties
			if (exludedProperties != null && exludedProperties.Length > 0)
				properties = properties.Where(i => !exludedProperties.Contains(i.Name)).ToArray();
			var results = new object[properties.Length];
			//loop properties
			var propertyList = properties.ToList();
			foreach (var propertyInfo in propertyList)
			{
				//get property and add property to object array results
				results[propertyList.IndexOf(propertyInfo)] = propertyInfo.GetValue(Object, null);
			}
			return results;
		}

		/// <summary>
		/// Converts the objects properties To a data column array.
		/// </summary>
		/// <param name="Object">The object.</param>
		/// <param name="exludedProperties">The exluded properties.</param>
		/// <returns></returns>
		public static DataColumn[] ToDataColumnArray(this object Object, params string[] exludedProperties)
		{
			//get properties
			const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
			var properties = Object.GetType().GetProperties(bindingFlags);
			//remove excluded Properties
			if (exludedProperties != null && exludedProperties.Length > 0)
				properties = properties.Where(i => !exludedProperties.Contains(i.Name)).ToArray();
			var results = new DataColumn[properties.Length];
			//loop properties
			var propertyList = properties.ToList();
			foreach (var propertyInfo in propertyList)
			{
				//get property and add property to object array results
				results[propertyList.IndexOf(propertyInfo)] = //propertyInfo.GetValue(Object, null)
					new DataColumn(propertyInfo.Name, (propertyInfo.PropertyType.IsGenericType 
									&& propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
						? Nullable.GetUnderlyingType(propertyInfo.PropertyType)
						: propertyInfo.PropertyType));
			}
			return results;
		}

		/// <summary>
		/// Firsts the name of the properties.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Object">The object.</param>
		/// <param name="index"> </param>
		/// <returns></returns>
		public static string PropertyName(this object Object, int index = 0)
		{
			return Object.GetType().GetProperties()[index].Name;
		}

		/// <summary>
		/// Converts a object to an Expando Object (Represents an object whose members can be dynamically added and removed at run time).
		/// </summary>
		/// <param name="anonymousObject">The anonymous object.</param>
		/// <returns></returns>
		public static ExpandoObject ToExpando(this object anonymousObject)
		{
			IDictionary<string, object> anonymousDictionary = new RouteValueDictionary(anonymousObject);
			IDictionary<string, object> expando = new ExpandoObject();
			anonymousDictionary.ForEach(expando.Add);
			return (ExpandoObject)expando;
		}
	}
}

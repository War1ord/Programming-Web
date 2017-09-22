using NHibernate.Linq;
using System.Collections.Generic;
using System.Data;

namespace BudgetManager.Extentions
{
	public static class ListExtentions
	{
		/// <summary>
		/// Converts the object to data table.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="exludedProperties">The exluded properties.</param>
		/// <returns></returns>
		public static DataTable ToDataTable<T>(this IReadOnlyList<T> items, string tableName, params string[] exludedProperties)
		{
			DataTable dataTable = new DataTable(tableName);
			dataTable.Columns.AddRange(items[0].ToDataColumnArray(exludedProperties));
			items.ForEach(item => {
				if (item != null) dataTable.Rows.Add(item.ToObjectArray(exludedProperties));
			});
			return dataTable;
		}

	}
}

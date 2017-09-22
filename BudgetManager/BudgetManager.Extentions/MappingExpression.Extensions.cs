using AutoMapper;
using System;
using System.Linq;

namespace BudgetManager.Extentions
{
	public static class MappingExpressionExtensions
	{
		public static void IgnoreAllUnmapped<TSource, TDestination>
			(this IMappingExpression<TSource, TDestination> expression)
		{
			Type sourceType = typeof (TSource);
			Type destinationType = typeof (TDestination);
			TypeMap existingMaps = Mapper.GetAllTypeMaps().First(map => map.SourceType == sourceType
			                                                            && map.DestinationType == destinationType);
			foreach (string property in existingMaps.GetUnmappedPropertyNames())
			{
				expression.ForMember(property, opt => opt.Ignore());
			}
		}
	}
}
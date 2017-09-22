using System;
using System.Diagnostics;
using System.Linq;

namespace BudgetManager.Common
{
	public static class CodeTime
	{
		public static long TestCode()
		{
			var i = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			var t = MeasureTime(() => i.ToList(), 100000000);
			return t;
		}
		public static long MeasureTime(Action action, int iterations)
		{
			var watch = new Stopwatch();
			watch.Start();
			for (int i = 0; i < iterations; i++) action();
			return watch.ElapsedMilliseconds;
		}
	}
}

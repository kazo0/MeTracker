using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeTracker
{
	public static class Extensions
	{
		public static IEnumerable<T> Safe<T>(this IEnumerable<T> list)
		{
			return list ?? Enumerable.Empty<T>();
		}

		public static bool None<T>(this IEnumerable<T> list)
		{
			return !list.Safe().Any();
		}
	}
}

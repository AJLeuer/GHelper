using System.Collections.Generic;

namespace GHelper.Utility
{
	public static class Extensions
	{
		public static void ReplaceAll<T>(this IList<T> collection, IEnumerable<T> items)
		{
			collection.Clear();
			foreach (T item in items)
			{
				collection.Add(item);
			}
		}
	}
}
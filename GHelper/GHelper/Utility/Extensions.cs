using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GHelper.Utility
{
	public static class Extensions
	{
		public static void ReplaceAll<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
		{
			collection.Clear();
			foreach (T item in items)
			{
				collection.Add(item);
			}
		}
	}
}
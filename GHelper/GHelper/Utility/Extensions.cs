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

		public static string ConvertPascalCaseToSentence(this string input)
		{
			string output = System.Text.RegularExpressions.Regex.Replace(
			                                                             input,
			                                                             "([^^])([A-Z])",
			                                                             "$1 $2"
			                                                            );

			return output;
		}
	}
}
using System.Reflection;

namespace GHelperLogic.Utility
{
	public static class Utility
	{
		/// Code credit StackOverflow user Ashdeep Singh
		/// https://stackoverflow.com/questions/930433/apply-properties-values-from-one-object-to-another-of-the-same-type-automaticall
		public static void CopyPropertiesTo(this object fromObject, object toObject)
		{
			PropertyInfo[] toObjectProperties = toObject.GetType().GetProperties();
			foreach (PropertyInfo propTo in toObjectProperties)
			{
				PropertyInfo propFrom = fromObject.GetType().GetProperty(propTo.Name)!;
				if (propFrom!=null && propFrom.CanWrite)
					propTo.SetValue(toObject, propFrom.GetValue(fromObject, null), null);
			}
		}
	}
}
using System.Collections.Generic;

namespace GHelperLogic.Utility
{
	public class Reference<T>
	{
		public T? Referent { get; set; }
		
		public Reference() {}
		public Reference(T referent)
		{
			Referent = referent!;
		}
	}
}
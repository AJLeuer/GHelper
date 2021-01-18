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

		public static implicit operator T (Reference<T> reference)
		{
			return reference!.Referent!;
		}
		
		public static implicit operator Reference<T> (T referent) {
			return new Reference<T>(referent);
		}
	}
}
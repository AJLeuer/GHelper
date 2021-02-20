using System;

namespace GHelper.Utility
{
	public interface ICloneable<T>
	{
		T Clone();

		void CopyStateFrom(T otherRecordViewModel);
	}
}
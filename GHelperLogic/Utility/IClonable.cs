namespace GHelperLogic.Utility
{
	public interface ICloneable<T>
	{
		T Clone();

		void CopyStateFrom(T otherRecordViewModel);
	}
}
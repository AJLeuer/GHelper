using System.IO;

namespace GHelperLogic.Utility
{
	public static class Utilities
	{
		/// Code credit StackOverflow user Gil Epshtain
		public static string AddIndexToFileNameIfNeeded(string sFileNameWithPath)
		{
			string sFileNameWithIndex = sFileNameWithPath;

			while (File.Exists(sFileNameWithIndex)) // run in while scoop so if after adding an index the the file name the new file name exist, run again until find a unused file name
			{ // File exist, need to add index

				string? sFilePath = Path.GetDirectoryName(sFileNameWithIndex);
				string sFileName = Path.GetFileNameWithoutExtension(sFileNameWithIndex);
				string sFileExtension = Path.GetExtension(sFileNameWithIndex);

				if (sFileName.Contains('_'))
				{ // Need to increase the existing index by one or add first index

					int iIndexOfUnderscore = sFileName.LastIndexOf('_');
					string sContentAfterUnderscore = sFileName.Substring(iIndexOfUnderscore + 1);

					// check if content after last underscore is a number, if so increase index by one, if not add the number _01
					int iCurrentIndex;
					bool bIsContentAfterLastUnderscoreIsNumber = int.TryParse(sContentAfterUnderscore, out iCurrentIndex);
					if (bIsContentAfterLastUnderscoreIsNumber)
					{
						iCurrentIndex++;
						string sContentBeforUnderscore = sFileName.Substring(0, iIndexOfUnderscore);

						sFileName = sContentBeforUnderscore + "_" + iCurrentIndex.ToString("000");
						sFileNameWithIndex = sFilePath + "\\" + sFileName + sFileExtension;
					}
					else
					{
						sFileNameWithIndex = sFilePath + "\\" + sFileName + "_001" + sFileExtension;
					}
				}
				else
				{ // No underscore in file name. Simple add first index
					sFileNameWithIndex = sFilePath + "\\" + sFileName + "_001" + sFileExtension;
				}
			}

			return sFileNameWithIndex;
		}
	}
}
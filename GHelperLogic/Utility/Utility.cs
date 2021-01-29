using System.IO;
using System.Linq;
using System.Text;

namespace GHelperLogic.Utility
{
	public class Utility
	{
		/// <summary>
		/// Code credit Stackoverflow user BullyWiiPlaza
		/// Found here https://stackoverflow.com/a/59538355/2562973
		/// </summary>
		/// <param name="resourceName"></param>
		/// <returns></returns>
		public static string? GetFileNameFromResourceName(string resourceName)
		{
			var stringBuilder = new StringBuilder();
			var escapeDot = false;
			var haveExtension = false;

			for (var resourceNameIndex = resourceName.Length - 1;
				resourceNameIndex >= 0;
				resourceNameIndex--)
			{
				if (resourceName[resourceNameIndex] == '_')
				{
					escapeDot = true;
					continue;
				}

				if (resourceName[resourceNameIndex] == '.')
				{
					if (!escapeDot)
					{
						if (haveExtension)
						{
							stringBuilder.Append('\\');
							continue;
						}

						haveExtension = true;
					}
				}
				else
				{
					escapeDot = false;
				}

				stringBuilder.Append(resourceName[resourceNameIndex]);
			}

			var fileName = Path.GetDirectoryName(stringBuilder.ToString());
			return fileName == null ? null : new string(fileName.Reverse().ToArray());
		}
	}
}
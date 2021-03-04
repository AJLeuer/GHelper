using System.Diagnostics;
using System.IO;
using NDepend.Path;

namespace GHelperLogic.Utility
{
	public static class Utilities
	{
		public static void TakeOwnershipOf(IAbsoluteFilePath file)
		{
			IAbsoluteFilePath gHelperExecutablePath = PathHelpers.ToAbsoluteFilePath(System.Reflection.Assembly.GetEntryAssembly()!.Location);
			IDirectoryPath executingDirectory = gHelperExecutablePath.ParentDirectoryPath;
			string filePermissionsUtilityExecutableFullPath = Path.Combine(executingDirectory.ToString()!, Properties.Resources.FilePermissionsUtilityExecutableName); 
			
			var filePermissionsUtilityStartInfo = new ProcessStartInfo
			                                      {
				                                      UseShellExecute = true,
				                                      FileName = filePermissionsUtilityExecutableFullPath,
				                                      Arguments = file.ToString()!,
				                                      Verb = Properties.Resources.ElevatedPermissionsVerb
			                                      };

			var filePermissionsUtilityProcess = new Process { StartInfo = filePermissionsUtilityStartInfo };
			filePermissionsUtilityProcess.Start();   
			filePermissionsUtilityProcess.WaitForExit();
		}
	}
}
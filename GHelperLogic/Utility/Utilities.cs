using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using NDepend.Path;

namespace GHelperLogic.Utility
{
	public static class Utilities
	{
		public static void TakeOwnershipOf(IAbsoluteFilePath file)
		{
			var filePermissionsUtilityStartInfo = new ProcessStartInfo
			                                      {
				                                      FileName = Properties.Resources.FilePermissionsUtilityExecutableName,
				                                      Arguments = file.ToString()!,
				                                      Verb = Properties.Resources.ElevatedPermissionsVerb
			                                      };

			var filePermissionsUtilityProcess = new Process { StartInfo = filePermissionsUtilityStartInfo };
			filePermissionsUtilityProcess.Start();   
			filePermissionsUtilityProcess.WaitForExit();
			
			
			if ((file.FileInfo is  { } fileInfo) && (WindowsIdentity.GetCurrent().User is { } currentUser))
			{
				// Get Currently Applied Access Control
				FileSecurity fileSecurity = FileSystemAclExtensions.GetAccessControl(fileInfo);

				//Update it, Grant Current User Full Control
				fileSecurity.SetOwner(currentUser);
				fileSecurity.SetAccessRule(new FileSystemAccessRule(currentUser, FileSystemRights.FullControl, AccessControlType.Allow));

				//Update the Access Control on the File
				file.FileInfo.SetAccessControl(fileSecurity);
			}
		}
	}
}
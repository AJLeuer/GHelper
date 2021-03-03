using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FilePermissionsUtility
{
	public static class FilePermissionsEditor
	{
		public static void TakeOwnership(FileInfo file)
		{
			if (WindowsIdentity.GetCurrent().User is SecurityIdentifier currentUser)
			{
				// Get Currently Applied Access Control
				FileSecurity fileSecurity = File.GetAccessControl(file.FullName);

				//Update it, Grant Current User Full Control
				fileSecurity.SetOwner(currentUser);
				fileSecurity.SetAccessRule(new FileSystemAccessRule(currentUser, FileSystemRights.FullControl, AccessControlType.Allow));

				//Update the Access Control on the File
				file.SetAccessControl(fileSecurity);
			}
		}
	}
}
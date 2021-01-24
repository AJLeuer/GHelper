using System;
using System.IO;
using NDepend.Path;

namespace GHelperLogic.Properties
{
    public static class Configuration
    {
	    public static IFilePath DefaultFilePath { get ; } =
		    PathHelpers.ToAbsoluteFilePath(
			    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
			    Path.DirectorySeparatorChar + Resources.GHubConfigFileDirectoryName + 
			    Path.DirectorySeparatorChar + Resources.GHubConfigFileName);
	    
	    public static IFilePath LogFilePath { get ; } =
		    PathHelpers.ToAbsoluteFilePath(
			    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
			    Path.DirectorySeparatorChar + Resources.GHelperAppDataDirectoryName + 
			    Path.DirectorySeparatorChar + Resources.GHelperLogFileName);
    }
}

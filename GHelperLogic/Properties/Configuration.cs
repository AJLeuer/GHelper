using System;
using System.IO;
using System.Reflection;
using NDepend.Path;

namespace GHelperLogic.Properties
{
    public static class Configuration
    {
	    public static IFilePath DefaultGHubSettingsFilePath { get ; } =
		    PathHelpers.ToAbsoluteFilePath(
			    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
			    Path.DirectorySeparatorChar + Resources.GHubConfigFileDirectoryName + 
			    Path.DirectorySeparatorChar + Resources.GHubConfigFileName);
	    
	    public static IFilePath DummyDebugGHubSettingsFilePath { get ; } =
		    PathHelpers.ToAbsoluteFilePath(
		        Path.Combine(
		            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, 
		            Resources.DummyGHUBSettingsRelativePath));

	    public static IFilePath LogFilePath { get ; } =
		    PathHelpers.ToAbsoluteFilePath(
			    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
			    Path.DirectorySeparatorChar + Resources.GHelperAppDataDirectoryName + 
			    Path.DirectorySeparatorChar + Resources.GHelperLogFileName);
    }
}

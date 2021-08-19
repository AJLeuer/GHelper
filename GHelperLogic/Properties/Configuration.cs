using System;
using System.IO;
using System.Reflection;
using NDepend.Path;

namespace GHelperLogic.Properties
{
    public static class Configuration
    {
	    public static IAbsoluteDirectoryPath GHubProgramDataDirectoryPath { get; } =
		    PathHelpers.ToAbsoluteDirectoryPath(Path.Combine(
		                                                     Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), 
		                                                     Resources.GHubDataDirectoryName));

	    public static IAbsoluteDirectoryPath GHubProgramDataDepotsDirectoryPath { get; } =
		    GHubProgramDataDirectoryPath.GetChildDirectoryWithName(Resources.GHubProgramDataDepotsSubdirectoryName);
	    public static IDirectoryPath GHubAppDataDirectoryPath { get; } =
		    PathHelpers.ToDirectoryPath(
		        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
		        Path.DirectorySeparatorChar + Resources.GHubDataDirectoryName);

	    public static IFilePath DefaultGHubSettingsFilePath { get ; } = GHubAppDataDirectoryPath.GetChildFileWithName(Resources.GHubConfigFileName);
	    public static IFilePath DefaultGHubSettingsDBFilePath { get ; } = GHubAppDataDirectoryPath.GetChildFileWithName(Resources.GHubConfigDBFileName);

	    public static IFilePath DummyDebugGHubSettingsFilePath { get ; } =
		    PathHelpers.ToAbsoluteFilePath(
		        Path.Combine(
		            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, 
		            Resources.DummyGHUBSettingsFileRelativePath));
	    
	    public static IFilePath DummyDebugGHubSettingsDBFilePath { get ; } =
		    PathHelpers.ToAbsoluteFilePath(
		        Path.Combine(
		            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, 
		            Resources.DummyGHUBSettingsDBFileRelativePath));


	    public static IDirectoryPath IconCacheDirectoryPath { get; } =
		    GHubAppDataDirectoryPath.GetChildDirectoryWithName(Resources.GHubIconCacheDirectoryName);
	    
	    public static IFilePath LogFilePath { get ; } =
		    PathHelpers.ToAbsoluteFilePath(
			    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
			    Path.DirectorySeparatorChar + Resources.GHelperAppDataDirectoryName + 
			    Path.DirectorySeparatorChar + Resources.GHelperLogFileName);

    }
}

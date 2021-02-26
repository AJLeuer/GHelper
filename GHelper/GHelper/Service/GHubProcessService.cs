using System.Diagnostics;
using GHelper.Properties;
using Nzr.ToolBox.Core;

namespace GHelper.Service
{
	public static class GHubProcessService
	{
		public static ProcessState GHubProcessState()
		{
			Process[] gHubProcesses = Process.GetProcessesByName(Resources.LogitechGHubProcessName);
			
			if (gHubProcesses.IsEmpty())
			{
				return ProcessState.Dead;
			}
			else
			{
				return ProcessState.Running;
			}
		}
	}

	public enum ProcessState
	{
		Running,
		Dead
	}
}
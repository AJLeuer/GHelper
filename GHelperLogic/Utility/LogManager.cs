using System.IO;
using GHelperLogic.Properties;


namespace GHelperLogic.Utility
{
    public static class LogManager
    {
	    private static FileStream?			 LogFile { get; set; }
        private static StreamWriter?         LogWriter = null;
        private static NodaTime.SystemClock? Clock;
  
        public static void Start()
        {
	        Configuration.LogFilePath.CreateContainingDirectoryIfNeeded();
            LogFile   = new FileStream(Configuration.LogFilePath.ToString()!, FileMode.OpenOrCreate, FileAccess.Write);
            LogWriter = new StreamWriter(LogFile);
            Clock     = NodaTime.SystemClock.Instance;
        }
  
        public static void Stop()
        {
            LogWriter?.Close();
        }
  
        public static void Log<OutputType>(OutputType output)
        {
	        LogWriter?.Write($"{Clock?.GetCurrentInstant().ToString()} ");
            LogWriter?.WriteLine(output);
            LogWriter?.Flush();
        }
    }
}
using System.IO;
using GHelperLogic.Model;

namespace GHelperLogic.IO
{
    public abstract class GHubSettingsIO
    {
        protected Stream? gHubSettingsStream;

        protected Stream GHubSettingsStream
        {
            get
            {
                if (gHubSettingsStream == null)
                {
                    gHubSettingsStream = InitializeGHubSettingsFileStream();
                }
                return gHubSettingsStream!;
            }
        }

        protected GHubSettingsFile? GHubSettingsFileObject;
        
        public State CheckSettingsAvailability(Stream? settingsStream = null)
        {
            try
            {
                settingsStream ??= GHubSettingsStream;
                if (settingsStream.CanRead)
                {
                    return State.Available;
                }
            }
            catch (IOException exception) {}
			
            return State.Unavailable;
        }
        public abstract GHubSettingsFile Read(Stream? settingsStream = null);
        public abstract void Write(Stream? settingsStream = null, GHubSettingsFile? settingsFileObject = null);

        protected abstract Stream? InitializeGHubSettingsFileStream();
        
        public enum State
        {
            Available,
            Unavailable
        }
    }
}
using System.IO;
using GHelperLogic.Model;
using Optional;

namespace GHelperLogic.IO
{
    public abstract class GHubSettingsIO
    {
        protected GHubSettingsFile? GHubSettingsFileObject;
        public abstract State CheckSettingsAvailability(Stream? settingsStream = null);

        public abstract Option<GHubSettingsFile> Read(Stream? settingsStream = null);
        public abstract void                     Write(Stream? settingsStream = null, GHubSettingsFile? settingsFileObject = null);

        public enum State
        {
            Available,
            Unavailable
        }

        private class DummyGHubSettingsIO : GHubSettingsIO
        {
            public override State CheckSettingsAvailability(Stream? settingsStream = null)
            {
                return State.Unavailable;
            }

            public override Option<GHubSettingsFile> Read(Stream? settingsStream = null)
            {
                return Option.None<GHubSettingsFile>();
            }

            public override void Write(Stream? settingsStream = null, GHubSettingsFile? settingsFileObject = null) { }
        }

        /// <summary>
        /// For more recent version of G Hub, Logitech switched to using a SQLite database file to store G Hub's state.
        /// Older versions used a JSON file. This method will determine whether we're dealing with a more recent version of G Hub
        /// by checking for the presence of a .db file or if we're dealing with an older version of G Hub in the case of the .db file being
        /// absent but the old .json file being present. For newer versions of G Hub, it'll return an instance of GHubSettingsDatabaseIO to
        /// handle the reading and writing of settings data. For older JSON-based versions of G Hub, it'll return a GHubSettingsFileReaderWriter
        /// instance. If neither are found, it'll return a dummy implementation with methods that are hard-coded to return nothing.
        /// </summary>
        /// <returns>GHubSettingsIO</returns>
        public static GHubSettingsIO CreateAppropriateInstanceForGHubVersion()
        {
            if (GHubSettingsDatabaseIO.CheckDatabaseFileAvailability() == State.Available)
            {
                return new GHubSettingsDatabaseIO();
            }
            else if (GHubSettingsFileReaderWriter.CheckFileAvailability() == State.Available)
            {
                return new GHubSettingsFileReaderWriter();
            }
            else
            {
                return new DummyGHubSettingsIO();
            }
        }
    }
}
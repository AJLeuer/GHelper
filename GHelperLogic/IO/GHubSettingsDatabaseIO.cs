using System;
using System.Collections.Generic;
using System.IO;
using GHelperLogic.Model;
using GHelperLogic.Utility;
using NDepend.Path;
using Optional;
using SqlNado;

namespace GHelperLogic.IO
{
    public class GHubSettingsDatabaseIO : GHubSettingsIO
    {
        private static readonly string PrimaryTableName = Properties.Resources.GHubConfigDBPrimaryTableName;

        private static readonly IFilePath GBHubSettingsDBFilePath =
            #if RELEASE || DEBUGRELEASE
                Properties.Configuration.DefaultGHubSettingsDBFilePath;
            #elif DEBUG
                Properties.Configuration.DummyDebugGHubSettingsDBFilePath;
            #endif
        
        public static State CheckDatabaseFileAvailability()
        {
            if (File.Exists(GBHubSettingsDBFilePath.ToString()))
            {
                return State.Available;
            }
            else
            {
                return State.Unavailable;
            }
        }

        private          SQLiteDatabase?              GHubSettingsDatabase { get; set; } 
        private readonly GHubSettingsFileReaderWriter GHubSettingsFileReaderWriter = new();
        
        ~GHubSettingsDatabaseIO()
        {
            GHubSettingsDatabase?.Dispose();
        }

        public override State CheckSettingsAvailability(Stream? settingsStream = null)
        {
            try
            {
                InitializeGHubSettingsDatabaseIfNeeded();
                return State.Available;
            }
            catch (Exception exception) when (exception is IOException or SQLiteException) 
            {
                LogManager.Log("Unable to open G Hub settings database.");
                return State.Unavailable;
            }

            void InitializeGHubSettingsDatabaseIfNeeded()
            {
                if (GHubSettingsDatabase is null)
                {
                    GHubSettingsDatabase = new SQLiteDatabase(GBHubSettingsDBFilePath.ToString(), SQLiteOpenOptions.SQLITE_OPEN_READWRITE);
                }
            }
        }

        public override Option<GHubSettingsFile> Read(Stream? settingsStream = null)
        {
            if (CheckSettingsAvailability() == State.Unavailable)
            {
                return Option.None<GHubSettingsFile>();
            }
            
            SQLiteTable? data = GHubSettingsDatabase?.GetTable(PrimaryTableName);
            IEnumerable<SQLiteRow>? rows = data?.GetRows();

            if (rows != null)
            {
                foreach (SQLiteRow row in rows)
                {
                    if ((row.Values[0] as int? ?? 0) == 1)
                    {
                        byte[] settingsFileRawData = (byte[]) row.Values[2];
                        var settingsFileDataStream = new MemoryStream(settingsFileRawData);
                        return GHubSettingsFileReaderWriter.Read(settingsFileDataStream);
                    }
                }
            }

            return Option.None<GHubSettingsFile>();
        }

        public override void Write(Stream? settingsStream = null, GHubSettingsFile? settingsFileObject = null)
        {
            throw new NotImplementedException();
        }
    }
}
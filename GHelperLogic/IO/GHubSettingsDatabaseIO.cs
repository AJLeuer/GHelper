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
    public class GHubSettingsDatabaseIO : GHubSettingsIO, IDisposable
    {
        private const string IDColumn = "_id";
        private const string FileColumn = "FILE";
        
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

        protected internal override GHubSettingsFile? GHubSettingsFileObject
        {
            get
            {
                return GHubSettingsFileReaderWriter?.GHubSettingsFileObject;
            }
            set
            {
                if (GHubSettingsFileReaderWriter is not null)
                {
                    GHubSettingsFileReaderWriter.GHubSettingsFileObject = value;
                }
            }
        }
        private           SQLiteDatabase?               GHubSettingsDatabase   { get; set; }
        private readonly  GHubSettingsFileReaderWriter? GHubSettingsFileReaderWriter = null;

        public GHubSettingsDatabaseIO()
        {
            InitializeGHubSettingsDatabaseIfNeeded();
            Stream? gHubSettingsFileStream = OpenSettingsStreamFromDatabase();
            if (gHubSettingsFileStream is not null)
            {
                GHubSettingsFileReaderWriter = new GHubSettingsFileReaderWriter(gHubSettingsFileStream: gHubSettingsFileStream);
            }
        }
        
        ~GHubSettingsDatabaseIO()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            GHubSettingsDatabase?.Dispose();
            GHubSettingsFileReaderWriter?.Dispose();
        }

        public override State CheckSettingsAvailability()
        {
            return GHubSettingsFileReaderWriter is null ? State.Unavailable : GHubSettingsFileReaderWriter.CheckSettingsAvailability();
        }

        public override Option<GHubSettingsFile> Read()
        {
            if ((GHubSettingsFileReaderWriter is null) || (CheckSettingsAvailability() == State.Unavailable))
            {
                return Option.None<GHubSettingsFile>();
            }
            else
            {
                return GHubSettingsFileReaderWriter.Read();
            }
        }

        public override void Write(GHubSettingsFile? settingsFileObject = null)
        {

            
        }
        
        private void InitializeGHubSettingsDatabaseIfNeeded()
        {
            try
            {
                if (GHubSettingsDatabase is null)
                {
                    GHubSettingsDatabase = new SQLiteDatabase (GBHubSettingsDBFilePath.ToString(), SQLiteOpenOptions.SQLITE_OPEN_READWRITE);
                }
            }
            catch (Exception)
            {
                LogManager.Log("Unable to open G Hub settings database.");
                throw;
            }
        }

        private Stream? OpenSettingsStreamFromDatabase()
        {
            try
            {
                SQLiteTable? settingsData = GHubSettingsDatabase?.GetTable(PrimaryTableName);
                IEnumerable<SQLiteRow>? rows = settingsData?.GetRows();

                if (rows != null)
                {
                    foreach (SQLiteRow row in rows)
                    {
                        if (row[IDColumn] is 1)
                        {
                            byte[] settingsFileRawData = (byte[]) row[FileColumn];
                            MemoryStream settingsFileDataStream = new (settingsFileRawData);
                            return settingsFileDataStream;
                        }
                    }
                }
            }
            catch (Exception)
            {
                LogManager.Log("Unable to read from G Hub settings database.");
            }
            
            return null;
        }
    }
}
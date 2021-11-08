using System;
using System.IO;
using GHelperLogic.Model;
using GHelperLogic.Model.Database;
using GHelperLogic.Utility;
using NDepend.Path;
using Optional;
using SqlNado;

namespace GHelperLogic.IO
{
    public class GHubSettingsDatabaseIO : GHubSettingsIO, IDisposable
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
                GHubSettingsFileReaderWriter = new GHubSettingsFileReaderWriter(gHubSettingsStream: gHubSettingsFileStream);
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
            GHubSettingsFileReaderWriter?.Write(settingsFileObject);
            MemoryStream? updatedSettingsDataStream = GHubSettingsFileReaderWriter?.GHubSettingsStream as MemoryStream;
            byte[]? updatedSettingsData = updatedSettingsDataStream?.ToArray();

            SQLiteTable? settingsDataTable = GHubSettingsDatabase?.GetTable(PrimaryTableName);
            GHubSettingsFileEntity? settingsRow = GHubSettingsFileEntity.FindSettingsFileRow(settingsDataTable);
            
            if (settingsRow is not null)
            {
                settingsRow.SettingsFileData = updatedSettingsData;
                GHubSettingsDatabase?.Save(settingsRow);
            }
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
            SQLiteTable? settingsDataTable = GHubSettingsDatabase?.GetTable(PrimaryTableName);
            GHubSettingsFileEntity? settingsRow = GHubSettingsFileEntity.FindSettingsFileRow(settingsDataTable);
            MemoryStream? settingsFileDataStream = (settingsRow?.SettingsFileData == null) ? null : new MemoryStream(settingsRow.SettingsFileData);
            return settingsFileDataStream;
        }


        
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using GHelperLogic.Model;
using NDepend.Path;
using SqlNado;

namespace GHelperLogic.IO
{
    public class GHubSettingsDatabaseIO : GHubSettingsIO
    {
        private static readonly string PrimaryTableName = Properties.Resources.GHubConfigDBPrimaryTableName;

        private GHubSettingsFileReaderWriter GHubSettingsFileReaderWriter = new();
            
        private readonly IFilePath GBHubSettingsDBFilePath =
            #if RELEASE || DEBUGRELEASE
                Properties.Configuration.DefaultGHubSettingsDBFilePath;
            #elif DEBUG
                Properties.Configuration.DummyDebugGHubSettingsDBFilePath;
            #endif
        
        public override GHubSettingsFile Read(Stream? settingsStream = null)
        {
            Stream? settingsFileDataStream = null;
            
            using (var database = new SQLiteDatabase(GBHubSettingsDBFilePath.ToString()))
            {
                SQLiteTable data = database.GetTable(PrimaryTableName);
                IEnumerable<SQLiteRow> rows = data.GetRows();
                foreach (SQLiteRow row in rows)
                {
                    if ((row.Values[0] as int? ?? 0) == 1)
                    {
                        byte[] settingsFileRawData = (byte[]) row.Values[2];
                        string settingsFileRawDataString = System.Text.Encoding.UTF8.GetString(settingsFileRawData, 0, settingsFileRawData.Length);
                        settingsFileDataStream = new MemoryStream(settingsFileRawData);
                    }
                }    
            }

            return GHubSettingsFileReaderWriter.Read(settingsFileDataStream);
        }

        public override void Write(Stream? settingsStream = null, GHubSettingsFile? settingsFileObject = null)
        {
            throw new NotImplementedException();
        }

        protected override Stream InitializeGHubSettingsFileStream()
        {
            return new FileStream(GBHubSettingsDBFilePath.ToString()!,
                                  FileMode.Open,
                                  FileAccess.ReadWrite);
        }
    }
}
using System;
using System.Collections.Generic;
using GHelperLogic.Utility;
using SqlNado;

namespace GHelperLogic.Model.Database
{
    [SQLiteTable(Name = "DATA")]
    public class GHubSettingsFileEntity
    {
        public const string IDColumn          = "_id";
        public const string DateCreatedColumn = "_date_created";
        public const string FileColumn        = "FILE";
        
        [SQLiteColumn(Name = IDColumn, IsPrimaryKey = true)]
        public int? ID { get; set; }

        [SQLiteColumn(Name = DateCreatedColumn)]
        public DateTime? DateCreated { get; set; }

        [SQLiteColumn(Name = FileColumn)]
        public byte[]? SettingsFileData { get; set; }

        public static implicit operator GHubSettingsFileEntity(SQLiteRow? settingsRow)
        {
            DateTime dateCreated;
            
            switch (settingsRow?[DateCreatedColumn])
            {
                case string dateString:
                    dateCreated = DateTime.Parse(dateString);
                    break;
                case Int64 numericDate:
                    dateCreated = DateTime.FromBinary(numericDate);
                    break;
                default:
                    dateCreated = default;
                    break;
            }
            
            return new GHubSettingsFileEntity
            {
                 ID = (int?) settingsRow?[IDColumn],
                 DateCreated = dateCreated,
                 SettingsFileData = (byte[]?) settingsRow?[FileColumn]
            };
        }
        
        public static GHubSettingsFileEntity? FindSettingsFileRow(SQLiteTable? settingsDataTable)
        {
            try
            {
                IEnumerable<SQLiteRow>? rows = settingsDataTable?.GetRows();
                if (rows != null)
                {
                    foreach (SQLiteRow? row in rows)
                    {
                        if (row[GHubSettingsFileEntity.IDColumn] is 1)
                        {
                            return row;
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
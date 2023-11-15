using LSL.EfCore.SettingsTable.Entities;
using Microsoft.EntityFrameworkCore;

namespace LSL.EfCore.SettingsTable
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Adds a Settings table to the EFCore DbContext
        /// </summary>
        /// <param name="source">ModeBuilder</param>
        /// <param name="tableName">Table name</param>
        /// <param name="keyFieldName">The name of the key field</param>
        /// <param name="valueFieldName">The name of the value field</param>
        /// <returns></returns>
        public static ModelBuilder AddSettingsTable(this ModelBuilder source, string tableName = "Settings", string keyFieldName = "Key", string valueFieldName = "Value")
        {
            var settingsTable = source.Entity<Setting>();
            
            settingsTable.ToTable(tableName);
            settingsTable.HasKey(s => s.Key);
            settingsTable.Property(s => s.Key).HasField(keyFieldName);
            settingsTable.Property(s => s.Value).HasField(valueFieldName);

            return source;
        }
    }
}

using System;
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
        public static ModelBuilder AddSettingsTable(this ModelBuilder source, string tableName = "Settings", string keyFieldName = "Key", string valueFieldName = "Value") => 
            source.AddSettingsTable<Setting>(tableName, keyFieldName, valueFieldName);

        /// <summary>
        /// Adds a Settings table to the EFCore DbContext using an ISetting implementing entity class
        /// </summary>
        /// <param name="source">ModeBuilder</param>
        /// <param name="tableName">Table name</param>
        /// <param name="keyFieldName">The name of the key field</param>
        /// <param name="valueFieldName">The name of the value field</param>
        /// <returns></returns>
        public static ModelBuilder AddSettingsTable<TSetting>(this ModelBuilder source, string tableName = "Settings", string keyFieldName = "Key", string valueFieldName = "Value")
            where TSetting : class, ISetting
        {
            return source.AddSettingsTable<TSetting>(c =>
            {
                c.ConfigureEntity(settingsTable =>
                {
                    settingsTable.ToTable(tableName);
                    settingsTable.HasKey(s => s.Key);
                    settingsTable.Property(s => s.Key).HasColumnName(keyFieldName);
                    settingsTable.Property(s => s.Value).HasColumnName(valueFieldName);
                });
            });
        }
        
        /// <summary>
        /// Entry point for finer-graned control of the setting entity and its EF Core configuration
        /// </summary>
        /// <param name="source"></param>
        /// <param name="configurator"></param>
        /// <typeparam name="TSettings"></typeparam>
        /// <returns></returns>
        public static ModelBuilder AddSettingsTable<TSettings>(this ModelBuilder source, Action<SettingsTableConfigurator<TSettings>> configurator)
            where TSettings : class
        {
            var config = new SettingsTableConfigurator<TSettings>();
            configurator(config);
            var entityBuilder = source.Entity<TSettings>();
            config.EntityConfigurator(entityBuilder);

            return source;
        }
    }
}
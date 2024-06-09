using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSL.EfCore.SettingsTable
{
    /// <summary>
    /// SettingsTableConfigurator
    /// </summary>
    public class SettingsTableConfigurator<TSettings> where TSettings : class
    {
        internal Action<EntityTypeBuilder<TSettings>> EntityConfigurator { get; private set; }

        /// <summary>
        /// Setup the TSettings entity for EF Core with the provided delegate
        /// </summary>
        /// <param name="entityConfigurator">Action to configure the entity with</param>
        /// <returns></returns>
        public SettingsTableConfigurator<TSettings> ConfigureEntity(Action<EntityTypeBuilder<TSettings>> entityConfigurator)
        {
            EntityConfigurator = entityConfigurator;
            return this;
        }
    }
}
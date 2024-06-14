using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LSL.EfCore.SettingsTable.Entities
{
    /// <summary>
    /// A Key/Value Setting
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Setting : ISetting
    {
        /// <inheritdoc/>
        [MaxLength(450)]
        public string Key { get; set; }

        /// <inheritdoc/>
        public string Value { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace LSL.EfCore.SettingsTable.Entities
{
    /// <summary>
    /// A Key/Value Setting
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// The setting key
        /// </summary>
        /// <value></value>
        [MaxLength(450)]
        public string Key { get; set; }

        /// <summary>
        /// The settings value
        /// </summary>
        /// <value></value>
        public string Value { get; set; }
    }
}

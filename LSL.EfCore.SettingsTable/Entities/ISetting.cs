namespace LSL.EfCore.SettingsTable.Entities
{
    /// <summary>
    /// A Key/Value Setting
    /// </summary>
    public interface ISetting
    {
        /// <summary>
        /// The setting key
        /// </summary>
        /// <value></value>        
        string Key { get; set; }

        /// <summary>
        /// The settings value
        /// </summary>
        /// <value></value>        
        string Value { get; set; }
    }
}

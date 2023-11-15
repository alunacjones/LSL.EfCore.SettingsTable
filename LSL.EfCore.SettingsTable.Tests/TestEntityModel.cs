using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LSL.EfCore.SettingsTable.Tests
{
    public class TestEntityModel
    {
        public string TableName { get; set; }
        public string Fields { get; set; } = string.Empty;
        public string Keys { get; set; } = string.Empty;
        public string Indexes { get; set; } = string.Empty;
        public string Constraints { get; set; } = string.Empty;
        public string ForeignKeys { get; set; } = string.Empty;

        public static TestEntityModel FromEntityType(IEntityType entityType)
        {
            return new TestEntityModel()
            {
                TableName = entityType.GetTableName(),
                Fields = string.Join(",", entityType.GetProperties().OrderBy(p => p.GetColumnName()).Select(p => $"{p.GetColumnName()}:{p.GetColumnType()}")),
                Keys = string.Join(",", entityType.GetKeys().Select(k => k.ToString())),
                Indexes = string.Join(",", entityType.GetIndexes().Select(i => i.ToString())),
                Constraints = string.Join(",", entityType.GetCheckConstraints().Select(cc => cc.ToString())),
                ForeignKeys = string.Join(",", entityType.GetDeclaredForeignKeys().Select(fk => fk.ToString()))
            };
        }
    }
}

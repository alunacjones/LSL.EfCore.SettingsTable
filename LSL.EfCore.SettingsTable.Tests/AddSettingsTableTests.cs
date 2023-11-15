using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LSL.EfCore.SettingsTable.Tests
{
    public class AddSettingsTableTests : ModelBuildingTest
    {
        [TestCase(typeof(DefaultTestContext), "Settings", "Key", "Value")]
        [TestCase(typeof(CustomTestContext), "CustomSettings", "CustomKey", "CustomValue")]            
        public void GivenADbContextThatAddsTheOutboxTable_ItShouldAddTheExpectedEntities(Type contextType, string expectedTableName, string expectedKeyName, string expectedValuName)
        {
            TestTheContext(
                contextType, 
                new[] 
                {
                    new TestEntityModel
                    {
                        TableName = expectedTableName,
                        Fields = $"{expectedKeyName}:nvarchar(450),{expectedValuName}:nvarchar(max)",
                        Keys = $"Key: Setting.Key PK"
                    }
                });
        }

        internal class DefaultTestContext : DbContext
        {
            public DefaultTestContext(DbContextOptions<DefaultTestContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.AddSettingsTable();
            }
        }

        internal class CustomTestContext : DbContext
        {
            public CustomTestContext(DbContextOptions<CustomTestContext> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.AddSettingsTable("CustomSettings", "CustomKey", "CustomValue");
            }
        }        
    }    
}

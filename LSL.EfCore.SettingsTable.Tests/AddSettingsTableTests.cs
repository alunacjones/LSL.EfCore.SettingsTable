using System;
using LSL.EfCore.SettingsTable.Entities;
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

        [Test]
        public void GivenADBContextWithMultipleCustomSettingsTables_ItShouldAddTheExpectedEntities()
        {
            TestTheContext(
                typeof(MultipleCustomSettingsTables), 
                new[] 
                {
                    new TestEntityModel
                    {
                        TableName = "CustomSettings",
                        Fields = $"Id:bigint,Name:nvarchar(max),Value:nvarchar(max)",
                        Keys = $"Key: CustomSettings.Id PK"
                    },
                    new TestEntityModel
                    {
                        TableName = "OtherSettings",
                        Fields = $"MyKey:nvarchar(450),MyValue:nvarchar(max)",
                        Keys = $"Key: OtherSettings.MyKey PK"
                    },   
                    new TestEntityModel
                    {
                        TableName = "YetAnotherSettingsTable",
                        Fields = "AnotherKey:nvarchar(450),AnotherValue:nvarchar(max),CreatedAt:datetime2",
                        Keys = "Key: SomeISettingImplementer.Key PK"
                    }
                });
        }

        internal class MultipleCustomSettingsTables : DbContext
        {
            internal class CustomSettings
            {
                public long Id { get; set; }
                public string Name { get; set; }
                public string Value { get; set; }                
            }

            internal class SomeISettingImplementer : ISetting
            {
                public string Key { get; set; }
                public string Value { get; set; }
                public DateTime CreatedAt { get; set; }
            }

            internal class OtherSettings 
            {
                public string MyKey { get; set; } = default!;
                public string MyValue { get; set; } = default!;
            }

            public MultipleCustomSettingsTables(DbContextOptions<MultipleCustomSettingsTables> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.AddSettingsTable<CustomSettings>(c => c.ConfigureEntity(e => e.HasKey(ee => ee.Id)));
                modelBuilder.AddSettingsTable<OtherSettings>(c => c.ConfigureEntity(e => e.HasKey(ee => ee.MyKey)));
                modelBuilder.AddSettingsTable<SomeISettingImplementer>();
                modelBuilder.AddSettingsTable<SomeISettingImplementer>("YetAnotherSettingsTable", "AnotherKey", "AnotherValue");
            }
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

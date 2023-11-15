using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace LSL.EfCore.SettingsTable.Tests
{
    public abstract class ModelBuildingTest
    {
        protected void TestTheContext(Type contextType, IEnumerable<TestEntityModel> expectedEntityModels)
        {
            var closedBuilderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(contextType);
            var optionsBuilder = (DbContextOptionsBuilder)Activator.CreateInstance(closedBuilderType);

            optionsBuilder.UseSqlServer("DataSource=:memory:");
            var sut = (DbContext)Activator.CreateInstance(contextType, optionsBuilder.Options);

            sut.Model.GetEntityTypes()
                .Select(et => TestEntityModel.FromEntityType(et))
                .Should()
                .BeEquivalentTo(expectedEntityModels);
        }
    }
}

[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-efcore-settingstable.svg)](https://ci.appveyor.com/project/alunacjones/lsl-efcore-settingstable)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.EfCore.SettingsTable)](https://coveralls.io/github/alunacjones/LSL.EfCore.SettingsTable)
[![NuGet](https://img.shields.io/nuget/v/LSL.EfCore.SettingsTable.svg)](https://www.nuget.org/packages/LSL.EfCore.SettingsTable/)

# LSL.EfCore.SettingsTable

This package provides `ModelBuilder` extension methods to allow you to produce a settings table.

>NOTE: You will still need to create migrations to ensure the table gets created. Please see [here](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) for further details.


To add the required entities to your `DbContext` then following should be added to `OnModelCreating` as below:

```csharp
    using LSL.EfCore.SettingsTable;
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...
        modelBuilder.AddSettingsTable();
        ...
    }
```
The default table name is `Settings` with a `Key` field called `Key` and a value field called `Value`. Each value can be customised by overriding the defaults as below:

```csharp
    using LSL.EfCore.SettingsTable;
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...
        modelBuilder.AddSettingsTable("CustomSettingsTable", "CustomKeyField", "CustomValueField");
        ...
    }
```

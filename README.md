How to use EFCore Migrations with layers
========================================

This sample show how to use EFCore Migrations when your application is split up into seperate assemblies or
architectural layers.

The layers
----------
This sample application is split into four assemblies.

Assembly         | Description
---------------- | -----------
MyApp.Data       | Contains the `DbContext`
MyApp.Migrations | Contains migration classes and the model snapshot
MyApp.Models     | Contains POCO entity type classes
MyApp.Web        | The web app. Configures the `DbContext`

Running commands
----------------
To run the commands, `MyApp.Migrations` should be used as the target project (**Default project** or `-Project` in PMC,
and `--project` in CLI), and `MyApp.Web` as the startup project (`-StartupProject` in PMC, and `--startup-project` in
CLI).

```sh
cd ./MyApp.Web/
dotnet ef migrations add Migration2 --project ../MyApp.Migrations/
```

Points of interest
------------------
There are a few parts of the sample that deserve some attention.

### References
Look at the project and package references. Anywhere you see `PrivateAssets="All"`, it means that that reference should
be available during development, but shouldn't get published to the server.

### Migrations assembly
In `Startup.ConfigureServices()`, we tell the `DbContext` where to find the migrations classes:

```cs
optionsBuilder.UseSqlServer(
    connectionString,
    x => x.MigrationsAssembly("MyApp.Migrations"));
```

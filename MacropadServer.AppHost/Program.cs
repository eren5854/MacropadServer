var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MacropadServer_WebAPI>("macropadserver-webapi");

builder.Build().Run();

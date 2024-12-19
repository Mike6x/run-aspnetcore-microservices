using Catalog.API;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterModules();

var app = builder.Build();

app.UseModules();

await app.RunAsync();
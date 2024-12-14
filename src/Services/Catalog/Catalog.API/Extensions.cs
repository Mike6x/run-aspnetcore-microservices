using System.Reflection;
using BuildingBlocks.OpenApi;
// using Asp.Versioning.Conventions;
// using BuildingBlocks.OpenApi;
using Carter;
using FluentValidation;

namespace Catalog.API;

public static class Extensions
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // define module assemblies
        // Add services to the container.
        var assembly = typeof(Program).Assembly;

        //register validators
        builder.Services.AddValidatorsFromAssembly(assembly);
        
        //register mediatr
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        //register module services
        builder.Services.ConfigureOpenApi();
        
        builder.RegisterCatalogServices();
        // builder.RegisterTodoServices();
        //
        // builder.RegisterSettingServices();
        // builder.RegisterElearningServices();

        //add carter endpoint modules
        builder.Services.AddCarter(configurator: config =>
        {
            config.WithModule<CatalogModule.Endpoints>();
            // config.WithModule<TodoModule.Endpoints>();
            //
            // config.WithModule<SettingModule.Endpoints>();
            // config.WithModule<ElearningModule.Endpoints>();

        });
        
        // builder.Services.AddCarter();

        builder.Services.AddMarten(opts =>
        {
            opts.Connection(builder.Configuration.GetConnectionString("Database")!);    
        }).UseLightweightSessions();

        if (builder.Environment.IsDevelopment())
            builder.Services.InitializeMartenWith<CatalogInitialData>();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

        return builder;
    }

    public static WebApplication UseModules(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        //register modules
        app.UseOpenApi();
        app.UseCatalogModule();
  
        // app.UseTodoModule();
        //
        // app.UseSettingModule();
        // app.UseElearningModule();

        //register api versions
        // var versions = app.NewApiVersionSet()
        //             .HasApiVersion(1)
        //             .HasApiVersion(2)
        //             .ReportApiVersions()
        //             .Build();

        // //map versioned endpoint
        // var endpoints = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(versions);

        //use carter
        //endpoints.MapCarter();

        return app;
    }
}

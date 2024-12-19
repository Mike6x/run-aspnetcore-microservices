using Asp.Versioning.Conventions;
using Basket.API.Domain;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using BuildingBlocks.Messaging.MassTransit;
using BuildingBlocks.OpenApi;

namespace Basket.API
{
    public static class Extensions
    {
        public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            // Add services to the container.
            var assembly = typeof(Program).Assembly;

            // register validators
            builder.Services.AddValidatorsFromAssembly(assembly);
            
            builder.Services.AddCarter();

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            // register versioning and swagger
            builder.Services.ConfigureOpenApi();
            
            //Data Services
            builder.Services.AddMarten(opts =>
            {
                opts.Connection(builder.Configuration.GetConnectionString("Database")!);
                opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
            }).UseLightweightSessions();

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
                //options.InstanceName = "Basket";
            });

            //Grpc Services
            builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                return handler;
            });

            //Async Communication Services
            builder.Services.AddMessageBroker(builder.Configuration);

            //Cross-Cutting Services
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
                .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

            return builder;
        }

        public static WebApplication UseModules(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app);

            // register api versions
            var apiVersionSet = app.NewApiVersionSet()
                        .HasApiVersion(1)
                        .HasApiVersion(2)
                        .ReportApiVersions()
                        .Build();

            // map versioned endpoint
            var versionGroup = app
                .MapGroup("api/v{version:apiVersion}")
                .WithApiVersionSet(apiVersionSet);

            // use carter
            versionGroup.MapCarter();
            
            app.UseExceptionHandler(options => { });

            app.UseHealthChecks("/api/v1/basket/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            
            // register swaggerUI, always last
            app.UseOpenApi();
            
            return app;
        }
    }
}
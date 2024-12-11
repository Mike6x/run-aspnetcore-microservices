using AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();

var redis = builder.AddRedis("redis");
var rabbitMq = builder.AddRabbitMQ("eventbus");

// var orderDbPassword = builder.AddParameter("orderDbPassword", secret: true);
// var orderDb = builder.AddSqlServer("sql", orderDbPassword)
//     .WithDataVolume()
//     .WithLifetime(ContainerLifetime.Persistent)
//     .AddDatabase("orderdb");
//
// var postgresAdmin = builder.AddParameter("postgresAdmin", secret: true);
//
// var basketDbPassword = builder.AddParameter("basketDbPassword", secret: true);
// var basketDb = builder.AddPostgres("postgresBasketDb", postgresAdmin, basketDbPassword, 5433)
//     .WithDataVolume()
//     .WithLifetime(ContainerLifetime.Persistent)
//     .AddDatabase("BasketDb");
//
// var catalogDbPassword = builder.AddParameter("catalogDbPassword", secret: true);
// var catalogDb = builder.AddPostgres("postgresCatalogDb", postgresAdmin, catalogDbPassword, 5432)
//     .WithImage("ankane/pgvector")
//     .WithImageTag("latest")
//     .AddDatabase("CatalogDb");

// Services

var basketApi = builder.AddProject<Projects.Basket_API>("basket-api")
    .WithReference(redis)
    .WithReference(rabbitMq)
    // .WithReference(basketDb)
    ;

var catalogApi = builder.AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(rabbitMq)
    // .WithReference(catalogDb)
    ;

var orderingApi = builder.AddProject<Projects.Ordering_API>("ordering-api")
    .WithReference(rabbitMq)
    // .WithReference(orderDb)
    ;

// Reverse proxies
builder.AddProject<Projects.YarpApiGateway>("reverse-proxy")
    .WithReference(catalogApi)
    .WithReference(orderingApi)
    .WithReference(basketApi)
    ;

// Apps
builder.AddProject<Projects.Shopping_Web>("mvc")
    .WithReference(basketApi)
    .WithReference(catalogApi)
    .WithReference(orderingApi)
    .WithReference(rabbitMq);

await using var app = builder.Build();

await app.RunAsync();
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API;

public static class CatalogModule
{
    public class Endpoints : CarterModule
    {
        public Endpoints() : base("catalog") { }
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            RouteGroupBuilder productGroup = app.MapGroup("products").WithTags("Products");
            productGroup.MapCreateProductEndpoint();
            productGroup.MapGetProductByIdEndpoint();
            productGroup.MapGetProductsEndpoint();
            productGroup.MapGetProductByCategoryEndpoint();
            productGroup.MapUpdateProductEndpoint();
            productGroup.MapDeleteProductEndpoint();
            // productGroup.MapExportProductsEndpoint();
            // productGroup.MapImportProductsEndpoint();

            // RouteGroupBuilder brandGroup = app.MapGroup("brands").WithTags("brands");
            // brandGroup.MapCreateBrandEndpoint();
            // brandGroup.MapGetBrandEndpoint();
            // brandGroup.MapGetBrandsEndpoint();
            // brandGroup.MapSearchBrandsEndpoint();
            // brandGroup.MapUpdateBrandEndpoint();
            // brandGroup.MapDeleteBrandEndpoint();
            // brandGroup.MapExportBrandsEndpoint();
            // brandGroup.MapImportBrandsEndpoint();
        }
    }
    public static WebApplicationBuilder RegisterCatalogServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        // builder.Services.BindDbContext<CatalogDbContext>();
        // builder.Services.AddScoped<IDbInitializer, CatalogDbInitializer>();
        //
        // builder.Services.AddKeyedScoped<IRepository<Product>, CatalogRepository<Product>>("catalog:products");
        // builder.Services.AddKeyedScoped<IReadRepository<Product>, CatalogRepository<Product>>("catalog:products");

        return builder;
    }
    
    public static WebApplication UseCatalogModule(this WebApplication app)
    {
        return app;
    }
}

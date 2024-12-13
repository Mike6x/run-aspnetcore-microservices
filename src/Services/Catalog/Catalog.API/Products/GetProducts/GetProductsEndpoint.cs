namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> Products);

// public class GetProductsEndpoint : ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
//         {
//             var query = request.Adapt<GetProductsQuery>();
//
//             var result = await sender.Send(query);
//
//             var response = result.Adapt<GetProductsResponse>();
//
//             return Results.Ok(response);
//         })
//         .WithName("GetProducts")
//         .Produces<GetProductsResponse>(StatusCodes.Status200OK)
//         .ProducesProblem(StatusCodes.Status400BadRequest)
//         .WithSummary("Get Products")
//         .WithDescription("Get Products");
//     }
// }


public static class GetProductsEndpoint
{
    internal static RouteHandlerBuilder MapGetProductsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/products", async (ISender sender, [AsParameters] GetProductsRequest request) =>
            {
                var query = request.Adapt<GetProductsQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName(nameof(GetProductsEndpoint))
            .WithSummary("Gets a list of products")
            .WithDescription("Gets a list of products with filtering support")
            .Produces<GetProductsResponse>()
            //.RequirePermission("Permissions.Products.Search")
            .MapToApiVersion(1);
    }
}

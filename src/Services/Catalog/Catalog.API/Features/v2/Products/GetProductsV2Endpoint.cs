using Catalog.API.Domain;
using Catalog.API.Features.v1.Products;

namespace Catalog.API.Features.v2.Products;

// public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
// public record GetProductsResponse(IEnumerable<Product> Products);

public static class GetProductsV2Endpoint
{
    internal static RouteHandlerBuilder MapGetProductsV2Endpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/", async (ISender sender, [AsParameters] GetProductsRequest request) =>
            {
                var query = request.Adapt<GetProductsV2Query>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName(nameof(GetProductsV2Endpoint))
            .WithSummary("Gets a list of products")
            .WithDescription("Gets a list of products with filtering support")
            .Produces<GetProductsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            //.RequirePermission("Permissions.Products.Search")
            .MapToApiVersion(2);
    }
}

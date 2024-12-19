using Catalog.API.Domain;

namespace Catalog.API.Features.v1.Products;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> Products);

public static class GetProductsEndpoint
{
    internal static RouteHandlerBuilder MapGetProductsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/", async (ISender sender, [AsParameters] GetProductsRequest request) =>
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
            .ProducesProblem(StatusCodes.Status400BadRequest)
            //.RequirePermission("Permissions.Products.Search")
            .MapToApiVersion(1);
    }
}

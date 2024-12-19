using Catalog.API.Domain;

namespace Catalog.API.Features.v1.Products;

//public record GetProductByCategoryRequest();
public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public static class GetProductByCategoryEndpoint
{
    internal static RouteHandlerBuilder MapGetProductByCategoryEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/category/{category}", 
                async (string category, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByCategoryQuery(category));
            
                    var response = result.Adapt<GetProductByCategoryResponse>();
            
                    return Results.Ok(response);
                })
            .WithName(nameof(GetProductByCategoryEndpoint))
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category")
            .Produces<GetProductByCategoryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .MapToApiVersion(1);
    }
}

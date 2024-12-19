using Catalog.API.Domain;

namespace Catalog.API.Features.v1.Products;

//public record GetProductByIdRequest();
public record GetProductByIdResponse(Product Product);

public static class GetProductByIdEndpoint
{
    internal static RouteHandlerBuilder MapGetProductByIdEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
            .WithName(nameof(GetProductByIdEndpoint))
            .WithSummary("gets product by id")
            .WithDescription("gets product by id")
            .Produces<GetProductByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            //.RequirePermission("Permissions.Products.View")
            .MapToApiVersion(1);
    }
}

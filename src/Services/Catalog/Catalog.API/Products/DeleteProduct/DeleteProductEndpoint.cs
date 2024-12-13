
namespace Catalog.API.Products.DeleteProduct;

//public record DeleteProductRequest(Guid Id);
public record DeleteProductResponse(bool IsSuccess);

// public class DeleteProductEndpoint : ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
//         {
//             var result = await sender.Send(new DeleteProductCommand(id));
//
//             var response = result.Adapt<DeleteProductResponse>();
//
//             return Results.Ok(response);
//         })
//         .WithName("DeleteProduct")
//         .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
//         .ProducesProblem(StatusCodes.Status400BadRequest)
//         .ProducesProblem(StatusCodes.Status404NotFound)
//         .WithSummary("Delete Product")
//         .WithDescription("Delete Product");
//     }
// }

public static class DeleteProductEndpoint
{
    internal static RouteHandlerBuilder MapDeleteProductEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
            .WithName(nameof(DeleteProductEndpoint))
            .WithSummary("deletes product by id")
            .WithDescription("deletes product by id")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            //equirePermission("Permissions.Products.Delete")
            .MapToApiVersion(1);
    }
}
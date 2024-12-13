
namespace Catalog.API.Products.GetProductById;

//public record GetProductByIdRequest();
public record GetProductByIdResponse(Product Product);

// public class GetProductByIdEndpoint : ICarterModule
// {
//     public void AddRoutes(IEndpointRouteBuilder app)
//     {
//         app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
//         {
//             var result = await sender.Send(new GetProductByIdQuery(id));
//
//             var response = result.Adapt<GetProductByIdResponse>();
//
//             return Results.Ok(response);
//         })
//         .WithName("GetProductById")
//         .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
//         .ProducesProblem(StatusCodes.Status400BadRequest)
//         .WithSummary("Get Product By Id")
//         .WithDescription("Get Product By Id");
//     }
// }

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

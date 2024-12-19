namespace Basket.API.Features.v1.Basket;

//public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(bool IsSuccess);

public static class DeleteBasketEndpoint
{    
    internal static RouteHandlerBuilder MapDeleteBasketEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint
            .MapDelete("/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(userName));

                var response = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(response);
            })
            .WithName(nameof(DeleteBasketEndpoint))
            .WithSummary("Delete Product")
            .WithDescription("Delete Product")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            //.RequirePermission("Permissions.Baskets.Delete")
            .MapToApiVersion(1);
    }
}

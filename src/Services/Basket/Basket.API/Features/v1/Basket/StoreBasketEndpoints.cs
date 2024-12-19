using Basket.API.Domain;
namespace Basket.API.Features.v1.Basket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);

public static class StoreBasketEndpoint
{
    internal static RouteHandlerBuilder MapStoreBasketEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint
            .MapPost("/", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/baskets/{response.UserName}", response);
            })
            .WithName(nameof(StoreBasketEndpoint))
            .WithSummary("Create Basket")
            .WithDescription("Create Basket")
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
           // .RequirePermission("Permissions.Dimensions.Update")
            .MapToApiVersion(1);
    }
}

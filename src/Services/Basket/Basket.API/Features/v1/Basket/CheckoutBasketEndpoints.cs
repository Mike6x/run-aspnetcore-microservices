using Basket.API.Basket.CheckoutBasket;
using Basket.API.Features.v1.Basket.Dtos;

namespace Basket.API.Features.v1.Basket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
public record CheckoutBasketResponse(bool IsSuccess);

public static class CheckoutBasketEndpoint
{
    internal static RouteHandlerBuilder MapCheckoutBasketEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint
            .MapPost("/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckoutBasketResponse>();

                return Results.Ok(response);
            })
            .WithName(nameof(CheckoutBasketEndpoint))
            .WithSummary("Checkout Basket")
            .WithDescription("Checkout Basket")
            .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            //.RequirePermission("Permissions.Basket.Checkout")
            .MapToApiVersion(1);
    }
}

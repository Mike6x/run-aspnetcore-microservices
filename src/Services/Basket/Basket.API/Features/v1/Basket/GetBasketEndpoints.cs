using Basket.API.Domain;
namespace Basket.API.Features.v1.Basket;

//public record GetBasketRequest(string UserName); 
public record GetBasketResponse(ShoppingCart Cart);

public static class GetBasketEndpoint
{
    internal static RouteHandlerBuilder MapGetBasketEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint
            .MapGet("/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));

                var respose = result.Adapt<GetBasketResponse>();

                return Results.Ok(respose);
            })
            .WithName(nameof(GetBasketEndpoint))
            .WithSummary("Get Basket By Id")
            .WithDescription("Get Basket By Id")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            //.RequirePermission("Permissions.Baskets.View")
            .MapToApiVersion(1);
    }
}

using Ordering.Application.Orders.V1;

namespace Ordering.API.Endpoints.V1.Ordering;

//- Accepts a name parameter.
//- Constructs a GetOrdersByNameQuery.
//- Retrieves and returns matching orders.

//public record GetOrdersByNameRequest(string Name);
public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public static class GetOrdersByNameEndpoint
{
    internal static RouteHandlerBuilder MapGetOrdersByNameEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint
            .MapGet("/{orderName}", async (string orderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(orderName));

                var response = result.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByNameEndpoint")
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Name")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .MapToApiVersion(1);
    }
}

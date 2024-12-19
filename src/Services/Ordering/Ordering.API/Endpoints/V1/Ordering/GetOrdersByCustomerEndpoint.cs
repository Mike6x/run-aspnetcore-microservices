using Ordering.Application.Orders.V1;

namespace Ordering.API.Endpoints.V1.Ordering;

//- Accepts a customer ID.
//- Uses a GetOrdersByCustomerQuery to fetch orders.
//- Returns the list of orders for that customer.

//public record GetOrdersByCustomerRequest(Guid CustomerId);
public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public static class GetOrdersByCustomerEndpoint
{
    internal static RouteHandlerBuilder MapGetOrdersByCustomerEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint
            .MapGet("/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);
            })
            .WithName(nameof(GetOrdersByCustomerEndpoint))
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .MapToApiVersion(1);
    }
}

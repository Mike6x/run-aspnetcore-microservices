using Ordering.Application.Orders.V1;

namespace Ordering.API.Endpoints.V1.Ordering;

//- Accepts a UpdateOrderRequest.
//- Maps the request to an UpdateOrderCommand.
//- Sends the command for processing.
//- Returns a success or error response based on the outcome.

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);

public static class UpdateOrderEndpint
{
    internal static RouteHandlerBuilder MapUpdateOrderEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint
            .MapPut("/", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateOrderResponse>();

                return Results.Ok(response);
            })
            .WithName(nameof(UpdateOrderEndpint))
            .WithSummary("Update Order")
            .WithDescription("Update Order")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .MapToApiVersion(1);
    }
}

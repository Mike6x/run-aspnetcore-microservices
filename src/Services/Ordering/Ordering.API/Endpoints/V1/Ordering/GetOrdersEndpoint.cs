using BuildingBlocks.Pagination;
using Ordering.Application.Orders.V1;

namespace Ordering.API.Endpoints.V1.Ordering;

//- Accepts pagination parameters.
//- Constructs a GetOrdersQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetOrdersRequest(PaginationRequest PaginationRequest);
public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public static class GetOrdersEndpoint
{
    internal static RouteHandlerBuilder MapGetOrdersEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint
            .MapGet("/", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));

                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            })
            .WithName(nameof(GetOrdersEndpoint))
            .WithSummary("Get Orders")
            .WithDescription("Get Orders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .MapToApiVersion(1);
    }
}

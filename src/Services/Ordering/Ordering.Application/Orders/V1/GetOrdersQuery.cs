using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.V1;

public record GetOrdersQuery(PaginationRequest PaginationRequest) 
    : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);
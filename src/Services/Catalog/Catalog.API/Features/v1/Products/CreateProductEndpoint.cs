// using MediatR;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Routing;
//
// namespace Catalog.API.Features.v1.Products;
//
// public static class CreateProductEndpoint
// {
//     internal static RouteHandlerBuilder MapCreateProductEndpoint(this IEndpointRouteBuilder endpoints)
//     {
//         return endpoints
//             .MapPost("/", async (CreateProductCommand request, ISender mediator) =>
//             {
//                 CreateProductResponse response = await mediator.Send(request);
//                 return Results.Ok(response);
//             })
//             .WithName(nameof(CreateProductEndpoint))
//             .WithSummary("creates a product")
//             .WithDescription("creates a product")
//             .Produces<CreateProductResponse>()
//             .RequirePermission("Permissions.Products.Create")
//             .MapToApiVersion(1);
//     }
// }
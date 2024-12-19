namespace Catalog.API.Features.v1.Products;

public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record CreateProductResponse(Guid Id);

public static class CreateProductEndpoint
{
    internal static RouteHandlerBuilder MapCreateProductEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName(nameof(CreateProductEndpoint))
            .WithSummary("creates a product")
            .WithDescription("creates a product")
            .Produces<CreateProductResponse>((StatusCodes.Status201Created))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            // .RequirePermission("Permissions.Products.Create")
            .MapToApiVersion(1);
    }
}

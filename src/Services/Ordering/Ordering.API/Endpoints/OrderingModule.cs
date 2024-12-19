using Ordering.API.Endpoints.V1.Ordering;

namespace Ordering.API.Endpoints;

public class OrderingModule
{
    public class Endpoints : CarterModule
    {
        public Endpoints() : base("ordering") { }
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var orderGroup = app.MapGroup("orders").WithTags("Order's API Group");
            orderGroup.MapCreateOrderEndpoint();
            orderGroup.MapGetOrdersByCustomerEndpoint();
            orderGroup.MapGetOrdersByNameEndpoint();
            orderGroup.MapGetOrdersEndpoint();
            orderGroup.MapUpdateOrderEndpoint();
            orderGroup.MapDeleteOrderEndpoint();
            // orderGroup.MapExportProductsEndpoint();
            // orderGroup.MapImportProductsEndpoint();
            
        }
    }
}
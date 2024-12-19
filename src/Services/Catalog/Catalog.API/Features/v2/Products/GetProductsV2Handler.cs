using Catalog.API.Domain;
using Catalog.API.Features.v1.Products;

namespace Catalog.API.Features.v2.Products;

public record GetProductsV2Query(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

internal class GetProductsV2QueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductsResult(products);
    }
}

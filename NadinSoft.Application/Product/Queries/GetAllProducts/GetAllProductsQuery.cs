using Final_NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Product.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(string? UserId = null, int? Page = null, int? Take = null) : IQuery<FilterProductResponse>;
}

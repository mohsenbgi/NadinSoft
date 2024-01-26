using Final_NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Product.Queries.GetProductById
{
    public sealed record GetProductByIdQuery(int Id) : IQuery<ProductResponse>;
}

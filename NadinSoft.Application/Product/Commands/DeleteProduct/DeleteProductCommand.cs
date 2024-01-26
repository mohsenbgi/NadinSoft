using NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Product.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(
        int Id,
        string UserId) : ICommand;
}

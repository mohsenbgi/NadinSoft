using NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Product.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
        string UserId,
        string Name,
        DateTime ProduceDate,
        string ManufacturePhone,
        string ManufactureEmail,
        bool IsAvailable) : ICommand<int>;
}

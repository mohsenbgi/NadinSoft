using NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Product.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(
        int Id,
        string UserId,
        string Name,
        DateTime ProduceDate,
        string ManufacturePhone,
        string ManufactureEmail,
        bool IsAvailable) : ICommand;
}

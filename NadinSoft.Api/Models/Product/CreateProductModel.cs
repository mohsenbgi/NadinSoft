namespace NadinSoft.Api.Models.Product
{
    public sealed record CreateProductModel(
        string Name,
        DateTime ProduceDate,
        string ManufacturePhone,
        string ManufactureEmail,
        bool IsAvailable
        );
}

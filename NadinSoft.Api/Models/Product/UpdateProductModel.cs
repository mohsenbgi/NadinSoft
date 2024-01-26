namespace NadinSoft.Api.Models.Product
{
    public sealed record UpdateProductModel(
        int Id,
        string Name,
        DateTime ProduceDate,
        string ManufacturePhone,
        string ManufactureEmail,
        bool IsAvailable
        );
}

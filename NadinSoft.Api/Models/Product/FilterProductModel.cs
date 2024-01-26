namespace NadinSoft.Api.Models.Product
{
    public sealed record FilterProductModel(
            string? UserId = null,
            int? Page = null,
            int? Take = null
        );
}

using FluentValidation;

namespace NadinSoft.Application.Product.Commands.DeleteProduct
{
    internal class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}

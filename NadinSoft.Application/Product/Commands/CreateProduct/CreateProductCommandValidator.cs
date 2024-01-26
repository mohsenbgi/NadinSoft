using FluentValidation;
using NadinSoft.Domain.Common;
using System.Text.RegularExpressions;

namespace NadinSoft.Application.Product.Commands.CreateProduct
{
    internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Name).NotEmpty()
                                .MaximumLength(200);

            RuleFor(x => x.ProduceDate).NotNull();

            RuleFor(x => x.ManufacturePhone).NotEmpty()
                                            .MaximumLength(11)
                                            .Must(phone => Regex.Match(phone, "^[0-9]9\\d{9}$").Success)
                                            .WithMessage(ErrorMessages.MobileFormatError);

            RuleFor(x => x.ManufactureEmail).NotEmpty()
                                            .MaximumLength(200)
                                            .EmailAddress()
                                            .WithMessage(ErrorMessages.EmailFormatError);
        }
    }
}

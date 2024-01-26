using FluentValidation;
using NadinSoft.Domain.Common;
using System.Text.RegularExpressions;

namespace NadinSoft.Application.Product.Commands.UpdateProduct
{
    internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Name).MaximumLength(200);

            RuleFor(x => x.ProduceDate);

            RuleFor(x => x.ManufacturePhone).MaximumLength(11)
                                            .Must(phone => Regex.Match(phone, "^[0-9]9\\d{9}$").Success)
                                            .WithMessage(ErrorMessages.MobileFormatError);

            RuleFor(x => x.ManufactureEmail).MaximumLength(200)
                                            .EmailAddress()
                                            .WithMessage(ErrorMessages.EmailFormatError);
        }
    }
}

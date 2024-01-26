using Final_NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Domain.Common;
using NadinSoft.Domain.Interfaces;

namespace NadinSoft.Application.Product.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var emailIsDuplicated = await _productRepository
                .AnyAsync(product => product.ManufactureEmail == request.ManufactureEmail && product.Id != request.Id);

            if (emailIsDuplicated)
                return Result.Failure(ErrorMessages.ManufactureEmailIsDuplicatedError);

            var phoneIsDuplicated = await _productRepository
                .AnyAsync(product => product.ManufacturePhone == request.ManufacturePhone && product.Id != request.Id);

            if (phoneIsDuplicated)
                return Result.Failure(ErrorMessages.ManufacturePhoneIsDuplicatedError);

            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                return Result.Failure(ErrorMessages.ItemNotFoundError);
            }

            if (product.CreatedById != request.UserId)
            {
                return Result.Failure(ErrorMessages.YouCanNotEditProductError);
            }

            product.Name = request.Name;
            product.ProduceDate = request.ProduceDate;
            product.ManufacturePhone = request.ManufacturePhone;
            product.ManufactureEmail = request.ManufactureEmail;
            product.IsAvailable = request.IsAvailable;

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return Result.Success();
        }
    }
}

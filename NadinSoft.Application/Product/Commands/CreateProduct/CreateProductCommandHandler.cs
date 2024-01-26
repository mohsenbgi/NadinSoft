using Final_NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Domain.Common;
using NadinSoft.Domain.Interfaces;

namespace NadinSoft.Application.Product.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                return Result.Failure<int>(ErrorMessages.ProcessFailedError);
            }

            if (await _productRepository.AnyAsync(product => product.ManufactureEmail == request.ManufactureEmail))
            {
                return Result.Failure<int>(ErrorMessages.ManufactureEmailIsDuplicatedError);
            }

            if (await _productRepository.AnyAsync(product => product.ManufacturePhone == request.ManufacturePhone))
            {
                return Result.Failure<int>(ErrorMessages.ManufacturePhoneIsDuplicatedError);
            }

            var product = new Domain.Entities.Product(
                        request.UserId,
                        request.Name,
                        request.ProduceDate,
                        request.ManufacturePhone,
                        request.ManufactureEmail,
                        request.IsAvailable
                        );

            await _productRepository.InsertAsync(product);
            await _productRepository.SaveAsync();

            return product.Id;
        }
    }
}

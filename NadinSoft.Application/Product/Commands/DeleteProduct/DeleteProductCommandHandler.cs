using Final_NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Domain.Common;
using NadinSoft.Domain.Interfaces;

namespace NadinSoft.Application.Product.Commands.DeleteProduct
{
    internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                return Result.Failure(ErrorMessages.ItemNotFoundError);
            }

            if (product.CreatedById != request.UserId)
            {
                return Result.Failure(ErrorMessages.YouCanNotEditProductError);
            }

            var isSuccess = await _productRepository.SoftDelete(product.Id);
            await _productRepository.SaveAsync();

            return isSuccess ? Result.Success() : Result.Failure(ErrorMessages.ProcessFailedError);
        }
    }
}

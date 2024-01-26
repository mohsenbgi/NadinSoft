using AutoMapper;
using Final_NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Domain.Common;
using NadinSoft.Domain.Interfaces;

namespace NadinSoft.Application.Product.Queries.GetProductById
{
    internal sealed class GetProductByIdQueryHandler
        : IQueryHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if(product is null)
            {
                return Result.Failure<ProductResponse>(ErrorMessages.ItemNotFoundError);
            }

            var response = _mapper.Map<ProductResponse>(product);

            return response;
        }
    }
}

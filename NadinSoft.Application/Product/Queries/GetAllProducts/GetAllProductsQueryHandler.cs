using AutoMapper;
using Final_NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Domain.Common;
using NadinSoft.Domain.Interfaces;

namespace NadinSoft.Application.Product.Queries.GetAllProducts
{
    internal sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, FilterProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<FilterProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var conditions = Filter.GenerateConditions<Domain.Entities.Product>();

            if (request.UserId != null)
            {
                conditions.Add(product => product.CreatedById == request.UserId);
            }

            var products = await _productRepository.FilterAsync(conditions, request.Page, request.Take);

            return _mapper.Map<FilterProductResponse>(products);
        }
    }
}

using AutoMapper;
using NadinSoft.Application.Product.Queries.GetAllProducts;
using NadinSoft.Application.Product.Queries.GetProductById;
using NadinSoft.Domain.Common;

namespace NadinSoft.Application.AutoMapper
{
    public sealed class EntityToResponseModelMappingProfile : Profile
    {
        public EntityToResponseModelMappingProfile()
        {
            CreateMap<Domain.Entities.Product, ProductResponse>();
            CreateMap<BasePaging<Domain.Entities.Product>, FilterProductResponse>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Entities;
using NadinSoft.Domain.Interfaces;

namespace NadinSoft.Infra.Data.Repositories
{
    public class ProductRepository : EfRepository<Product, int>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}

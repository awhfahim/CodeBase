using Exam1.Domain.Entities;
using Exam1.Domain.Repositories;
using FirstDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IList<Product>records, int total, int totalDisplay)> GetPagedProductsAsync
            (int pageIndex, int pageSize, string name, uint priceFrom, uint priceTo, string orderBy)
        {
            Expression<Func<Product, bool>>? expression = null;

            if(!string.IsNullOrEmpty(name))
            {
                expression = x => x.Name.Contains(name) && (x.Price >= priceFrom && x.Price <= priceTo);
            }

            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }
    }
}

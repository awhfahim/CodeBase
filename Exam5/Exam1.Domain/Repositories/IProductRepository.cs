using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Domain.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product, Guid>
    {
        Task<(IList<Product> records, int total, int totalDisplay)> GetPagedProductsAsync
            (int pageIndex, int pageSize, string name, uint priceFrom, uint priceTo, string orderBy);
    }
}

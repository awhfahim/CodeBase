using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features
{
    public interface IProductManagementService
    {
        Task CreateProductAsync(string name, uint price, double weight);
        Task DeleteProductAsync(Guid id);
        Task<(IList<Product> records, int total, int totalDisplay)> GetPagedProductsAsync
            (int pageIndex, int pageSize, string name, uint priceFrom, uint priceTo, string v);
        Task<Product> GetProductById(Guid id);
        Task UpdateProductAsync(Guid Id, string name, uint price, double weight);
    }
}

using Exam1.Domain.Entities;
using FirstDemo.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public ProductManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateProductAsync(string name, uint price, double weight)
        {
            var product = new Product { Name = name, Price = price, Weight = weight };
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _unitOfWork.ProductRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Product> records, int total, int totalDisplay)> GetPagedProductsAsync
            (int pageIndex, int pageSize, string name, uint priceFrom, uint priceTo, string orderBy)
        {
            return await _unitOfWork.ProductRepository.GetPagedProductsAsync(pageIndex, pageSize, name, priceFrom, priceTo, orderBy);
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task UpdateProductAsync(Guid Id, string name, uint price, double weight)
        {
            var product = await GetProductById(Id);
            if(product != null)
            {
                product.Weight = weight;
                product.Price = price;
                product.Name = name;
            }
            await _unitOfWork.SaveAsync();
        }
    }
}

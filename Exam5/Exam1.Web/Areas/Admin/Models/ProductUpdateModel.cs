using Autofac;
using Exam1.Application.Features;
using Exam1.Domain.Entities;

namespace Exam1.Web.Areas.Admin.Models
{
    public class ProductUpdateModel
    {
        private IProductManagementService _productService;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public uint Price { get; set; }
        public double Weight { get; set; }

        public ProductUpdateModel()
        {
            
        }
        public ProductUpdateModel(IProductManagementService productManagementService)
        {
            _productService = productManagementService;        
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _productService = scope.Resolve<IProductManagementService>();
        }

        internal async Task LoadAsync(Guid id)
        {
            var product = await _productService.GetProductById(id);
            if (product != null) 
            {
                Name = product.Name;
                Price = product.Price;
                Weight = product.Weight;
            }
        }

        internal async Task UpdateProductAsync()
        {
            await _productService.UpdateProductAsync(Id,Name, Price, Weight);
        }
    }
}

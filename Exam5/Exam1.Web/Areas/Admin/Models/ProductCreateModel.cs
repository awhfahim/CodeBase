
using Autofac;
using Exam1.Application.Features;

namespace Exam1.Web.Areas.Admin.Models
{
    public class ProductCreateModel
    {
        private IProductManagementService _productService;
        public string Name { get; set; }
        public uint Price { get; set; }
        public double Weight { get; set; }

        public ProductCreateModel()
        {
            
        }
        public ProductCreateModel(IProductManagementService productManagement)
        {
            _productService = productManagement;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _productService = scope.Resolve<IProductManagementService>();
        }

        internal async Task CreateProductAsync()
        {
            await _productService.CreateProductAsync(Name, Price, Weight);
        }
    }
}

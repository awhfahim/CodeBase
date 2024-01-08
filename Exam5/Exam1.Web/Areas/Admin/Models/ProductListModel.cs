using Autofac;
using Exam1.Application.Features;
using Exam1.Infrastructure.Utilities;

namespace Exam1.Web.Areas.Admin.Models
{
    public class ProductListModel
    {
        private IProductManagementService _productManagementService;
        public SearchProduct SearchProduct { get; set; }
        public ProductListModel()
        {
            
        }
        public ProductListModel(IProductManagementService productManagementService)
        {
            _productManagementService = productManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _productManagementService = scope.Resolve<IProductManagementService>();
        }

        internal async Task<object> GetPagedProductsAsync(DataTablesAjaxRequestUtility dataTableModel)
        {
            var data = await _productManagementService.GetPagedProductsAsync(dataTableModel.PageIndex,
                dataTableModel.PageSize,
                SearchProduct.Name,
                SearchProduct.PriceFrom,
                SearchProduct.PriceTo,
                dataTableModel.GetSortText(new string[] {"Name", "Price"}));

            return new
            {
                total = data.total,
                totalDisplay = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Name.ToString(),
                            record.Price.ToString(),
                            record.Weight.ToString(),
                            record.Id.ToString()
                        })
            };
        }

        internal async Task DeleteProductAsync(Guid id)
        {
            await _productManagementService.DeleteProductAsync(id);
        }

    }
}

using Autofac;
using Exam1.Infrastructure.Utilities;
using Exam1.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> logger;
        private readonly ILifetimeScope scope;

        public ProductController(ILogger<ProductController> logger, ILifetimeScope scope)
        {
            this.logger = logger;
            this.scope = scope;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = scope.Resolve<ProductCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            model.Resolve(scope);
            if(ModelState.IsValid)
            {
                try
                {
                    await model.CreateProductAsync();
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Product Added",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = $"Server Error {ex}",
                        Type = ResponseTypes.Danger
                    });
                }
                
            }

            TempData.Put("ResponseMessage", new ResponseModel
            {
                Message = $"Server Error ",
                Type = ResponseTypes.Danger
            });
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetProducts(ProductListModel model)
        {
            var dataTableModel = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(scope);

            var data = await model.GetPagedProductsAsync(dataTableModel);
            return Json(data);
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            var model = scope.Resolve<ProductListModel>();
            try
            {
                await model.DeleteProductAsync(Id);
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully Deleted Product",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Server Error",
                    Type = ResponseTypes.Danger
                });
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid Id)
        {
            var model = scope.Resolve<ProductUpdateModel>();
            await model.LoadAsync(Id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductUpdateModel model)
        {
            model.Resolve(scope);
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateProductAsync();
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Updated Product",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Server Error",
                        Type = ResponseTypes.Danger
                    });
                }
                
            }
            return View(model);
        }
    }
}

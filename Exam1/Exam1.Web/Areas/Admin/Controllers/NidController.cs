using Autofac;
using Exam1.Infrastructure.Utilities;
using Exam1.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NidController : Controller
    {
        private readonly ILogger<NidController> logger;
        private readonly ILifetimeScope scope;

        public NidController(ILogger<NidController> logger, ILifetimeScope scope)
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
            var model = scope.Resolve<CreateNidModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNidModel model)
        {
            model.Resolve(scope);
            if (ModelState.IsValid)
            {
                await model.CreateNidAsync();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Succefully Added",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> GetNID(NIDListModel model)
        {
            var dataTable = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(scope);

            var data = await model.GetTableDataAsync(dataTable);
            return Json(data);
        }
        
        public async Task<IActionResult> Update(Guid Id)
        {
            var model = scope.Resolve<NIDUpdateModel>();
            await model.GetNIDByID(Id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(NIDUpdateModel model, [FromRoute]Guid Id)
        {
            model.Resolve(scope);

            if(ModelState.IsValid)
            {
                await model.UpdateNIDAsync(Id);
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Succesfully Update",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            var model = scope.Resolve<NIDListModel>();
            await model.DeleteNIDAsync(Id);
            TempData.Put("ResponseMessage", new ResponseModel
            {
                Message = "Succesfully Deleted NID",
                Type = ResponseTypes.Success
            });
            return RedirectToAction("Index");
        }
    }
}

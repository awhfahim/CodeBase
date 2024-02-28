using Autofac;
using Library.Domain;
using Library.Infrastructure;
using Library.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> logger;
        private readonly ILifetimeScope scope;

        public BookController(ILogger<BookController> logger, ILifetimeScope scope)
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
            var model = scope.Resolve<BookCreateModel>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateModel model)
        {
            model.Resolve(scope);
            if (ModelState.IsValid)
            {
                try
                {
                    await model.CreateBookAsync();

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Created Book",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch(DuplicateTitleException ex)
                {
                    logger.LogCritical(ex, "Title is Duplicate");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
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

        public async Task<JsonResult> GetBooks()
        {
            var model = scope.Resolve<BookListModel>();
            var dataTableModel = new DataTablesAjaxRequestUtility(Request);

            var data = await model.GetPagedBooksAsync(dataTableModel);
            return Json(data);
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            var model = scope.Resolve<BookListModel>();
            try
            {
                await model.DeleteBookAsync(Id);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Book Deleted Successfully",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch(Exception ex)
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
            var model = scope.Resolve<BookUpdateModel>();
            await model.LoadBookAsync(Id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BookUpdateModel model)
        {
            model.Resolve(scope);
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateBookAsync();

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Updated Book",
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

            TempData.Put("ResponseMessage", new ResponseModel
            {
                Message = "Model State is Not Valid. (Check DateTime Format)",
                Type = ResponseTypes.Danger
            });

            return View(model);
        }
    }
}

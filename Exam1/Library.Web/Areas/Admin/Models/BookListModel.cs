using Autofac;
using Library.Application.Features.LibraryService;
using Library.Infrastructure;
using System.Web;

namespace Library.Web.Areas.Admin.Models
{
    public class BookListModel
    {
        private IBookManagementService _bookManagementService;

        public BookListModel()
        {
            
        }
        public BookListModel(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _bookManagementService = scope.Resolve<IBookManagementService>();
        }

        internal async Task<object> GetPagedBooksAsync(DataTablesAjaxRequestUtility dataTableModel)
        {
            var data = await _bookManagementService.GetPagedBooksAsync(dataTableModel.PageIndex,
                dataTableModel.PageSize,
                dataTableModel.SearchText,
                dataTableModel.GetSortText(new string[] {"Title", "AuthorName", "PublishDate"}));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from r in data.record
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(r.Title),
                            HttpUtility.HtmlEncode(r.AuthorName),
                            r.PublishDate.ToString(),
                            r.Id.ToString()

                        })
            };
        }

        internal async Task DeleteBookAsync(Guid id)
        {
            await _bookManagementService.DeleteBookAsync(id);
        }
    }
}

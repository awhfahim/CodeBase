using Autofac;
using Library.Application.Features.LibraryService;

namespace Library.Web.Areas.Admin.Models
{
    public class BookCreateModel
    {
        private IBookManagementService _bookManagementService;

        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorName { get; set; }

        public BookCreateModel()
        {
            
        }
        public BookCreateModel(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _bookManagementService = scope.Resolve<IBookManagementService>();
        }

        internal async Task CreateBookAsync()
        {
            await _bookManagementService.CreateBookAsync(Title, PublishDate, AuthorName);
        }
    }
}

using Autofac;
using Library.Application.Features.LibraryService;

namespace Library.Web.Areas.Admin.Models
{
    public class BookUpdateModel
    {
        private IBookManagementService _bookManagementService;

        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorName { get; set; }

        public BookUpdateModel()
        {
            
        }

        public BookUpdateModel(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _bookManagementService = scope.Resolve<IBookManagementService>();
        }

        internal async Task LoadBookAsync(Guid Id)
        {
            var book = await _bookManagementService.LoadBookAsync(Id);
            if(book != null)
            {
                Title = book.Title;
                PublishDate = book.PublishDate;
                AuthorName = book.AuthorName;
            }
        }

        internal async Task UpdateBookAsync()
        {
            await _bookManagementService.UpdateBookAsync(Id, Title, PublishDate, AuthorName);
        }
    }
}

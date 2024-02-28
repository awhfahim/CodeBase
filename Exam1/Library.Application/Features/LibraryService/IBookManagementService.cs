
using Library.Domain.Entities;

namespace Library.Application.Features.LibraryService
{
    public interface IBookManagementService
    {
        Task CreateBookAsync( string Title, DateTime publishDate, string AuthorName);
        Task DeleteBookAsync(Guid id);
        Task<(IList<Book> record, int total, int totalDisplay)> 
            GetPagedBooksAsync(int pageIndex, int pageSize, string searchText, string orderBy);
        Task<Book> LoadBookAsync(Guid id);
        Task UpdateBookAsync(Guid Id, string title, DateTime publishDate, string authorName);
    }
}
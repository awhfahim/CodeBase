using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book, Guid>
    {
        Task<(IList<Book> record, int total, int totalDisplay)> 
            GetPagedBooksAsync(int pageIndex, int pageSize, string searchText, string orderBy);
        Task<bool> IsDuplicateAsync(string title);
    }
}

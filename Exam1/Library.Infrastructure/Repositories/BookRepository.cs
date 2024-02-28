using Library.Domain.Entities;
using Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book, Guid>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IList<Book> record, int total, int totalDisplay)> 
            GetPagedBooksAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            return await GetDynamicAsync(x => x.Title.Contains(searchText), orderBy, null, pageIndex, pageSize, true);
        }

        public async Task<bool> IsDuplicateAsync(string title)
        {
            return (await GetCountAsync(x => x.Title == title)) > 0;
        }
    }
}

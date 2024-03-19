using Library.Domain;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.LibraryService
{
    public class BookManagementService : IBookManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public BookManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateBookAsync(string title, DateTime publishDate, string authorName)
        {
            bool isDuplicate = await _unitOfWork.BookRepository.IsDuplicateAsync(title);

            if (isDuplicate)
            {
                throw new DuplicateTitleException("Title is Duplicate");
            }
            var book = new Book() { Title = title, AuthorName = authorName, PublishDate = publishDate };
            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteBookAsync(Guid id)
        {
            await _unitOfWork.BookRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Book> record, int total, int totalDisplay)> 
            GetPagedBooksAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            return await _unitOfWork.BookRepository.GetPagedBooksAsync(pageIndex, pageSize, searchText, orderBy);
        }

        public async Task<Book> LoadBookAsync(Guid id)
        {
            return await _unitOfWork.BookRepository.GetByIdAsync(id);
        }

        public async Task UpdateBookAsync(Guid Id, string title, DateTime publishDate, string authorName)
        {
            var book = await LoadBookAsync(Id);
            
            if(book is null)
            {
                throw new NullReferenceException();
            }
            if(book is not null) 
            {
                book.Title = title;
                book.AuthorName = authorName;
                book.PublishDate = publishDate;
            }
            await _unitOfWork.SaveAsync();
        }
    }
}

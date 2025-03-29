using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book> AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<BorrowedBook> AddAsync(BorrowedBook borrowedBook)
        {
            await _context.BorrowedBooks.AddAsync(borrowedBook);
            await _context.SaveChangesAsync();
            return borrowedBook;
        }

        public async Task<BorrowedRequest> AddAsync(BorrowedRequest borrowedRequest)
        {
            await _context.BorrowedRequests.AddAsync(borrowedRequest);
            await _context.SaveChangesAsync();
            return borrowedRequest;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public async Task<IEnumerable<BorrowedBook>> GetListBorrowedBookAsync()
        {
            return await _context.BorrowedBooks.ToArrayAsync();
        }

        public async Task<BorrowedBook?> GetBorrowedBookAsync(Guid id)
        {
            var borrowedBook = await _context.BorrowedBooks.FirstOrDefaultAsync(x => x.Id == id);
            return borrowedBook;
        }

        public async Task<IEnumerable<BorrowedRequest>> GetListBorrowedRequestAsync()
        {
            return await _context.BorrowedRequests.ToListAsync();
        }

        public async Task<BorrowedRequest> GetBorrowedRequestAsync(Guid id)
        {
            var borrowedRequest = await _context.BorrowedRequests.FirstOrDefaultAsync(x => x.Id == id);
            return borrowedRequest;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            var result = await _context.Books.FindAsync(book.Id);
            if (result == null)
            { return null; }

            result.Title = book.Title;
            result.Description = book.Description;
            result.Author = book.Author;
            result.Publisher = book.Publisher;
            result.BorrowedQuantity = book.BorrowedQuantity;
            result.Year = book.Year;
            result.Category = book.Category;
            result.Quantity = book.Quantity;
            return result;
            
        }

        public async Task<BorrowedBook> UpdateBorrowedBookAsync(BorrowedBook borrowedBook)
        {
            var result = await _context.BorrowedBooks.FindAsync(borrowedBook.Id);
            if (result == null) { return null; }

            result.StartDate = borrowedBook.StartDate;
            result.EndDate = borrowedBook.EndDate;
            result.BorrowedRequestId = borrowedBook.BorrowedRequestId;
            return result;
        }

        public async Task<BorrowedRequest> UpdateBorrowedRequestAsync(BorrowedRequest borrowedRequest)
        {
            var result = await _context.BorrowedRequests.FindAsync(borrowedRequest.Id);
            if (result == null) { return null; };

            result.BookId = borrowedRequest.BookId;
            result.UserId = borrowedRequest.UserId;
            result.Status = borrowedRequest.Status;
            result.RequestDate = borrowedRequest.RequestDate;

            return result;
        }

        public async Task<BorrowedRequest> AddBorrowedRequestAsync(BorrowedRequest borrowedRequest)
        {
            await _context.BorrowedRequests.AddAsync(borrowedRequest);
            await _context.SaveChangesAsync();
            return borrowedRequest;
        }

        public async Task<BorrowedBook> AddBorrowedBookAsync(BorrowedBook borrowedBook)
        {
            await _context.BorrowedBooks.AddAsync(borrowedBook);
            await _context.SaveChangesAsync();
            return borrowedBook;
        }
    }
}
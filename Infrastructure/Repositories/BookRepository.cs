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

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return book;
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

    }
}
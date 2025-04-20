using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BorrowedBookRepository: IBorrowedBookRepository
    {
        private readonly AppDbContext _context;

        public BorrowedBookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BorrowedBook> AddAsync(BorrowedBook borrowedBook)
        {
            await _context.BorrowedBooks.AddAsync(borrowedBook);
            await _context.SaveChangesAsync();
            return borrowedBook;
        }

        public async Task<IEnumerable<BorrowedBook>> GetAllAsync()
        {
            return await _context.BorrowedBooks.ToListAsync();
        }

        public async Task<BorrowedBook?> GetAsync(Guid id)
        {
            var borrowedBook = await _context.BorrowedBooks.FirstOrDefaultAsync(x => x.Id == id);
            return borrowedBook;
        }


        public async Task<BorrowedBook> UpdateAsync(BorrowedBook borrowedBook)
        {
            var result = await _context.BorrowedBooks.FindAsync(borrowedBook.Id);
            if (result == null) { return null; }

            result.StartDate = borrowedBook.StartDate;
            result.EndDate = borrowedBook.EndDate;
            result.BorrowedRequestId = borrowedBook.BorrowedRequestId;
            return result;
        }

    }
}
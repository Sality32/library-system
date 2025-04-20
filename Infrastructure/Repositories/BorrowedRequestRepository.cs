using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BorrowedRequestRepository: IBorrowedRequestRepository
    {
        private readonly AppDbContext _context;

        public BorrowedRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BorrowedRequest> AddAsync(BorrowedRequest borrowedRequest)
        {
            await _context.BorrowedRequests.AddAsync(borrowedRequest);
            await _context.SaveChangesAsync();
            return borrowedRequest;
        }


        public async Task<IEnumerable<BorrowedRequest>> GetAllAsync()
        {
            return await _context.BorrowedRequests.ToListAsync();
        }

        public async Task<BorrowedRequest?> GetAsync(Guid id)
        {

            var borrowedRequest = await _context.BorrowedRequests.FirstOrDefaultAsync(x => x.Id == id);
            return borrowedRequest;
        }

        public async Task<BorrowedRequest> UpdateAsync(BorrowedRequest borrowedRequest)
        {
            var result = await _context.BorrowedRequests.FindAsync(borrowedRequest.Id);
            if (result == null) { return null; };

            result.BookId = borrowedRequest.BookId;
            result.UserId = borrowedRequest.UserId;
            result.Status = borrowedRequest.Status;
            result.RequestDate = borrowedRequest.RequestDate;

            return result;
        }

    }
}
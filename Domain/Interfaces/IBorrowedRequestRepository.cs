using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBorrowedRequestRepository
    {
        Task<BorrowedRequest> AddAsync(BorrowedRequest borrowedRequest);
        Task<IEnumerable<BorrowedRequest>> GetAllAsync();
        Task<BorrowedRequest?> GetAsync(Guid id);
        Task<BorrowedRequest?> UpdateAsync(BorrowedRequest borrowedRequest);
    }
}
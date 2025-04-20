using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBorrowedBookRepository
    {
        Task<BorrowedBook> AddAsync(BorrowedBook borrowedBook);
        Task<IEnumerable<BorrowedBook>> GetAllAsync();
        Task<BorrowedBook?> GetAsync(Guid id);
        Task<BorrowedBook?> UpdateAsync(BorrowedBook borrowedBook);
    }
}
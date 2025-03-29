using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> AddAsync(Book book);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetAsync(Guid id);
        Task<Book?> UpdateAsync(Book book);
        Task<IEnumerable<BorrowedBook>> GetListBorrowedBookAsync();
        Task<IEnumerable<BorrowedRequest>> GetListBorrowedRequestAsync();
        Task<BorrowedBook> AddAsync(BorrowedBook borrowedBook);
        Task<BorrowedRequest> AddAsync(BorrowedRequest borrowedRequest);
        Task<BorrowedBook?> GetBorrowedBookAsync(Guid id);
        Task<BorrowedRequest?> GetBorrowedRequestAsync(Guid id);
        Task<BorrowedBook?> UpdateBorrowedBookAsync(BorrowedBook borrowedBook);
        Task<BorrowedRequest?> UpdateBorrowedRequestAsync(BorrowedRequest borrowedRequest);
        Task<BorrowedRequest> AddBorrowedRequestAsync(BorrowedRequest borrowedRequest);
        Task<BorrowedBook> AddBorrowedBookAsync(BorrowedBook borrowedBook);
    }
}
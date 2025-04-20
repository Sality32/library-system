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
    }
}
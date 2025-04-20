using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRequestAddCollectionRepository
    {
        Task<RequestAddCollection> AddAsync(RequestAddCollection requestAddCollection);
        Task<IEnumerable<RequestAddCollection>> GetAllAsync();
        Task<RequestAddCollection?> GetAsync(Guid id);
        Task<RequestAddCollection?> UpdateAsync(RequestAddCollection requestAddCollection);
    }
}
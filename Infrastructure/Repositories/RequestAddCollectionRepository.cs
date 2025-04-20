using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RequestAddCollectionRepository: IRequestAddCollectionRepository
    {
        private readonly AppDbContext _context;

        public RequestAddCollectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RequestAddCollection> AddAsync(RequestAddCollection requestAddCollection)
        {
            await _context.RequestAddCollections.AddAsync(requestAddCollection);
            await _context.SaveChangesAsync();
            return requestAddCollection;
        }

        public async Task<IEnumerable<RequestAddCollection>> GetAllAsync()
        {
            return await _context.RequestAddCollections.ToListAsync();
        }

        public async Task<RequestAddCollection?> GetAsync(Guid id)
        {
            var requestAddCollection = await _context.RequestAddCollections.FirstOrDefaultAsync(x => x.Id == id);
            return requestAddCollection;
        }

        public async Task<RequestAddCollection> UpdateAsync(RequestAddCollection requestAddCollection)
        {
            var result = await _context.RequestAddCollections.FindAsync(requestAddCollection.Id);
            if (result == null)
            { return null; }

            result.DetailBooksKey = requestAddCollection.DetailBooksKey;
            result.Status = requestAddCollection.Status;
            result.RequestDate = requestAddCollection.RequestDate;
            return result;
            
        }

    }
}
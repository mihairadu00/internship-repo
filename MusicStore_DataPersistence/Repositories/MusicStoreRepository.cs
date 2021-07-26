using Microsoft.EntityFrameworkCore;
using MusicStore_Common.Entities;
using MusicStore_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore_DataPersistence.Repositories
{
    public class MusicStoreRepository : IMusicStoreRepository
    {

        private readonly MusicStoreDbContext _context;

        public MusicStoreRepository(MusicStoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add the new item to the collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Item> AddAsync(Item item)
        {

            item.Id = Guid.NewGuid();

            await _context.AddAsync(item);

            await _context.SaveChangesAsync();

            return item;
        
        }

        /// <summary>
        /// Remove the item with the given id from the collection.
        /// Throws KeyNotFoundException if the item cannot be found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(Guid id)
        {

            var foundItem = await _context.Items
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundItem == null)
                return false;

            _context.Remove(foundItem);
            await _context.SaveChangesAsync();

            return true;

        }

        /// <summary>
        /// Get the collection of items.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Item>> GetAsync()
        {

            var result = await _context.Items
                .ToListAsync();

            return result;

        }

        /// <summary>
        /// Get the collection of items, in a paged format.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Item>> GetAsync(int pageNumber, int pageSize)
        {

            var result = await _context.Items
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result;
       
        }

        /// <summary>
        /// Get the item by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Item> GetByIdAsync(Guid id)
        {

            var result = await _context.Items
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;

        }

        /// <summary>
        /// Update the existing item with the new values.
        /// Throws KeyNotFoundException if the item cannot be found.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Item item)
        {

            var foundItem = await _context.Items
                .FirstOrDefaultAsync(x => x.Id == item.Id);

            if (foundItem == null)
                return false;

            foundItem.Price = item.Price;

            await _context.SaveChangesAsync();

            return true;

        }
    }
}

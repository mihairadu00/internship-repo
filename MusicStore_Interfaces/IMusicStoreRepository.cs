using MusicStore_Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore_Interfaces
{
    public interface IMusicStoreRepository
    {

        /// <summary>
        /// Get the item by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Item> GetByIdAsync(Guid id);

        /// <summary>
        /// Get the collection of items.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetAsync();

        /// <summary>
        /// Get the collection of items, in a paged format.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Add the new item to the collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Item> AddAsync(Item item);

        /// <summary>
        /// Update the existing item with the new values.
        /// Throws KeyNotFoundException if the item cannot be found.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Item item);

        /// <summary>
        /// Remove the item with the given id from the collection.
        /// Throws KeyNotFoundException if the item cannot be found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(Guid id);

    }
}

using MusicStore_Common.DTOs;
using MusicStore_Common.Util;
using MusicStore_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore_Services
{
    public class MusicStoreService : IMusicStoreService
    {

        private readonly IMusicStoreRepository _musicStoreRepository;

        public MusicStoreService(IMusicStoreRepository musicStoreRepository)
        {
            _musicStoreRepository = musicStoreRepository;
        }

        /// <summary>
        /// Add the new item to the collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<ItemDto> AddAsync(ItemDto itemDto)
        {
            var item = itemDto.ToEntity();
            var addedItem = await _musicStoreRepository.AddAsync(item);
            return addedItem.ToDto();
        }

        /// <summary>
        /// Get the collection of items.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var itemsList = await _musicStoreRepository.GetAsync();
            return itemsList.Any() ? itemsList.Select(x => x.ToDto()) : new List<ItemDto>();
        }

        /// <summary>
        /// Get the collection of items, in a paged format.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemDto>> GetAsync(int pageNumber = 1, int pageSize = 100)
        {
          
            var results = await _musicStoreRepository.GetAsync(pageNumber, pageSize);
            return results.Any() ? results.Select(x => x.ToDto()) : new List<ItemDto>();
        
        }

        /// <summary>
        /// Get the item by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ItemDto> GetByIdAsync(Guid id)
        {

            var result = await _musicStoreRepository.GetByIdAsync(id);
            return result != null ? result.ToDto() : null;
        
        }

        /// <summary>
        /// Remove the item with the given id from the collection.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            return await _musicStoreRepository.RemoveAsync(id);
        }

        /// <summary>
        /// Update the item animal with the new values.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ItemDto item)
        {
            return await _musicStoreRepository.UpdateAsync(item.ToEntity());
        }
    }
}

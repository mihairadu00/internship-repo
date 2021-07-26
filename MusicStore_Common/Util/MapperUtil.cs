using MusicStore_Common.DTOs;
using MusicStore_Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore_Common.Util
{
    public static class MapperUtil
    {

        public static Item ToEntity(this ItemDto dto)
        {
            return new Item
            {
                Id = dto.Id ?? Guid.Empty,
                Name = dto.Name,
                Producer = dto.Producer,
                Type = dto.Type,
                Price = dto.Price,
                UserId = dto.AddedById,
                CreatedDate = dto.ListingDate
            };
        }

        public static ItemDto ToDto(this Item entity)
        {
            return new ItemDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Producer = entity.Producer,
                Type = entity.Type,
                Price = entity.Price,
                AddedById = entity.UserId,
                ListingDate = entity.CreatedDate
            };
        }

    }
}

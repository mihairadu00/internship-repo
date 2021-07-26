using MusicStore_API.Models;
using MusicStore_Common.DTOs;
using System;
using System.Collections.Generic;

namespace MusicStore_API.Util
{

    public static class MapperUtil
    {

        public static ItemModel ToModel(this ItemDto dto)
        {
            return new ItemModel
            {
                Id = dto.Id ?? Guid.Empty,
                Name = dto.Name,
                Producer = dto.Producer,
                Type = dto.Type,
                Price = dto.Price,
                ListingDate = dto.ListingDate
            };
        }

        public static ItemDto ToDto(this ItemModel model, Guid? id)
        {
            return new ItemDto
            {
                Id = id,
                Name = model.Name,
                Producer = model.Producer,
                Type = model.Type,
                Price = model.Price,
                ListingDate = model.ListingDate
            };
        }

    }

}
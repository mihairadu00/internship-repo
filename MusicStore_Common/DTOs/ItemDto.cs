using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore_Common.DTOs
{
    public class ItemDto
    {

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public Guid AddedById { get; set; }
        public DateTime ListingDate { get; set; }

    }
}

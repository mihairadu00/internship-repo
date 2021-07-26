using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MusicStore_API.Models
{

    public class ItemModel
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Producer { get; set; }

        public string Type { get; set; }

        [Required]
        public double Price { get; set; }

        public DateTime ListingDate { get; set; }

    }

}
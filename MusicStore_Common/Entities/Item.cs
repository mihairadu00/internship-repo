using MusicStore_Common.DBInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore_Common.Entities
{
    public class Item : ICreatedDateStamp, IModifiedDateStamp, ISoftDelete
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

using MusicStore_Common.DBInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MusicStore_Common.Entities
{
    public class User : ICreatedDateStamp
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string UserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string Salt { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}

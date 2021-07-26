using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore_Common
{
    public class AuthorizationSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenLifetimeInMinutes { get; set; }
    }
}

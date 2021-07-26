using System.ComponentModel.DataAnnotations;

namespace MusicStore_API.Models
{

    public class AuthenticationRequest
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }

}
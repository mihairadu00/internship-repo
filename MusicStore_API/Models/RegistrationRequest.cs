using System.ComponentModel.DataAnnotations;

namespace MusicStore_API.Models
{

    public class RegistrationRequest
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }

}
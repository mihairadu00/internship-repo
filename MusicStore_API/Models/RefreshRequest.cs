using System.ComponentModel.DataAnnotations;

namespace MusicStore_API.Models
{

    public class RefreshRequest
    {

        [Required]
        public string Username { get; set; }

    }

}
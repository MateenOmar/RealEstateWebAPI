using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class User
    {
        public int ID { get; set; } 

        [Required]
        public string username { get; set; }

        [Required]
        public byte[] password { get; set; }

        public byte[] passwordKey { get; set; }
        
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Photos")]
    public class Photo : BaseEntity
    {        
        [Required]
        public string PublicID { get; set; }
        [Required]
        public string ImageURL { get; set; }
        public bool IsPrimary { get; set; }
        public int PropertyID { get; set; }
        public Property Property { get; set; }

    }
}
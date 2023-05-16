using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class BaseEntity
    {
        [Column(Order = 0)]
        public int ID { get; set; }
        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
        public int LastUpdatedBy { get; set; }

    }
}
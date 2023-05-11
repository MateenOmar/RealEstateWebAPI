namespace WebAPI.Models
{
    public class City
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime LastUpdatedOn { get; set; }
        
        public int LastUpdatedBy { get; set; }

    }
}

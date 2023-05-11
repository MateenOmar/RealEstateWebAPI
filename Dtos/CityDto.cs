using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos
{
    public class CityDto
    {
        public int ID { get; set; }

        [Required (ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
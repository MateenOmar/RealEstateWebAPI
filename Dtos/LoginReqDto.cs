using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos
{
    public class LoginReqDto
    {
        public string username { get; set; }

        public string password { get; set; }   
    }
}
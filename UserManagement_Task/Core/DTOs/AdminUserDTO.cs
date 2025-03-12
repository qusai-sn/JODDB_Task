using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.DTOs
{
    public class AdminUserDTO
    {
        public int AdminID { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? CreatedAt { get; set; }


    }
}



 

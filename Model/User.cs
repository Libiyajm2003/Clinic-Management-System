using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public char Gender { get; set; }
        public DateTime JoiningDate { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }   // 3 = Receptionist, 2 = Doctor, etc.
        public bool IsActive { get; set; } = true;
    }
}

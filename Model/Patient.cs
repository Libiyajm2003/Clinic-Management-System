using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    public class Patient
    {
        public string MMRNumber { get; set; }
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }      // New
        public string Phone { get; set; }
        public string Address { get; set; }
        public int? MembershipId { get; set; }
        public bool IsActive { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    public class Patient
    {
        public string MMRNumber { get; set; }      // Existing
        public int PatientId { get; set; }         // New auto-increment field
        public string FullName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }        // New
        public int? MembershipId { get; set; }     // New
        public bool IsActive { get; set; }
    }
}

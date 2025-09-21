using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string DoctorName { get; set; }    
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; } 

        public decimal ConsultationFee { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

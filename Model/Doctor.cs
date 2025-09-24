using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a doctor including profile, specialization and fee details.
    /// </summary>
    public class Doctor
    {
        /// <summary>
        /// Unique doctor identifier.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Associated user account identifier for authentication.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Full name of the doctor.
        /// </summary>
        public string DoctorName { get; set; }    

        /// <summary>
        /// Specialization identifier.
        /// </summary>
        public int SpecializationId { get; set; }

        /// <summary>
        /// Readable specialization name.
        /// </summary>
        public string SpecializationName { get; set; } 

        /// <summary>
        /// Consultation fee for an appointment with the doctor.
        /// </summary>
        public decimal ConsultationFee { get; set; }

        /// <summary>
        /// Indicates whether the doctor profile is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}

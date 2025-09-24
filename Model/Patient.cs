using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a patient registered in the clinic management system.
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Unique Medical MR number assigned to the patient.
        /// </summary>
        public string MMRNumber { get; set; }

        /// <summary>
        /// Internal patient row identifier.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Patient full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Patient gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Patient age derived from date of birth.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Patient date of birth.
        /// </summary>
        public DateTime DOB { get; set; }

        /// <summary>
        /// Primary contact phone number (10 digits).
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Residential address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Optional membership identifier if enrolled.
        /// </summary>
        public int? MembershipId { get; set; }

        /// <summary>
        /// Indicates whether the patient profile is active.
        /// </summary>
        public bool IsActive { get; set; }
    }

}

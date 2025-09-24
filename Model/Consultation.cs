using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents the consultation details captured during a doctor's visit.
    /// </summary>
    public class Consultation
    {
        /// <summary>
        /// Unique consultation identifier.
        /// </summary>
        public int ConsultationId { get; set; }

        /// <summary>
        /// Related appointment identifier.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Reported symptoms from the patient.
        /// </summary>
        public string Symptoms { get; set; }

        /// <summary>
        /// Doctor's diagnosis.
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// Additional notes, advice, or observations.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Timestamp when the consultation was created.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Indicates whether the record is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}

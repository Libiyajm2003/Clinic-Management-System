using System;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a prescribed medicine for an appointment.
    /// </summary>
    public class MedicinePrescription
    {
        /// <summary>
        /// Unique identifier for the medicine prescription.
        /// </summary>
        public int MedicinePrescriptionId { get; set; }

        /// <summary>
        /// Identifier of the prescribed medicine definition.
        /// </summary>
        public int MedicineId { get; set; }

        /// <summary>
        /// Custom medicine name when free-typed by doctor.
        /// </summary>
        public string MedicineName { get; set; }

        /// <summary>
        /// Dosage instructions (e.g., 1-0-1 after food).
        /// </summary>
        public string Dosage { get; set; }

        /// <summary>
        /// Duration for the prescription (e.g., 5 days).
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Quantity prescribed.
        /// </summary>
        public int Qty { get; set; } = 1;

        /// <summary>
        /// Related appointment identifier.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Whether the prescription record is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}

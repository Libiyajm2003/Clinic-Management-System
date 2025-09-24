using System;

namespace ConsoleAppCms2025.Model
{
    /// <summary>
    /// Represents a billing record generated for an appointment.
    /// </summary>
    public class Billing
    {
        /// <summary>
        /// Unique bill identifier.
        /// </summary>
        public int BillId { get; set; }

        /// <summary>
        /// Related appointment identifier.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Patient MMR number associated with the bill.
        /// </summary>
        public string PatientMMR { get; set; }

        /// <summary>
        /// Doctor identifier for the consultation.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Fee charged for the consultation.
        /// </summary>
        public decimal ConsultationFee { get; set; }

        /// <summary>
        /// Total medicine charges accumulated for the appointment.
        /// </summary>
        public decimal MedicineCharges { get; set; }

        /// <summary>
        /// Total lab charges accumulated for the appointment.
        /// </summary>
        public decimal LabCharges { get; set; }

        /// <summary>
        /// Total amount payable for the bill.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Date when the bill is generated.
        /// </summary>
        public DateTime BillDate { get; set; }

        /// <summary>
        /// Payment status of the bill.
        /// </summary>
        public bool IsPaid { get; set; }
    }
}




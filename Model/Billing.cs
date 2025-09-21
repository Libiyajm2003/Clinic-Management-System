using System;

namespace ConsoleAppCms2025.Model
{
    public class Billing
    {
        public int BillId { get; set; }
        public int AppointmentId { get; set; }
        public string PatientMMR { get; set; }
        public int DoctorId { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal LabCharges { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BillDate { get; set; }
        public bool IsPaid { get; set; }
    }
}




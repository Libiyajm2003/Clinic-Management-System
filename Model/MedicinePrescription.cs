using System;

namespace ConsoleAppCms2025.Model
{
    public class MedicinePrescription
    {
        public int MedicinePrescriptionId { get; set; }  // PK
        public int MedicineId { get; set; }              // FK
        public string MedicineName { get; set; }         // User input
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public int Qty { get; set; } = 1;
        public int AppointmentId { get; set; }           // Appointment FK
        public bool IsActive { get; set; } = true;
    }
}

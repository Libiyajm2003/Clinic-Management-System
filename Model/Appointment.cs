using System;

namespace ConsoleAppCms2025.Model
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string TokenNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string ConsultationType { get; set; }
        public string PatientMMR { get; set; }
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        // ✅ New property for visited status
        public bool IsVisited { get; set; }

        // Optional helper to show status as string
        public string VisitStatus => IsVisited ? "Visited" : "Not Visited";
    }
}

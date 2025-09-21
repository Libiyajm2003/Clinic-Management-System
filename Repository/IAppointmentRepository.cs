using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    public interface IAppointmentRepository
    {
        // Get all appointments for a doctor
        Task<List<Appointment>> GetAppointmentsByDoctorAsync(int doctorId);

        // Get a single appointment by ID
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);

        // Book a new appointment
        Task<Appointment> BookAppointmentAsync(Appointment appointment);

        // Mark an appointment as visited
        Task<bool> MarkVisitedAsync(int appointmentId);
    }
}



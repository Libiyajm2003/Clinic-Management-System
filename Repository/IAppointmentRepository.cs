using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// Data access operations for appointments.
    /// </summary>
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Gets all appointments for the specified doctor.
        /// </summary>
        Task<List<Appointment>> GetAppointmentsByDoctorAsync(int doctorId);

        /// <summary>
        /// Gets a single appointment by its identifier.
        /// </summary>
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);

        /// <summary>
        /// Inserts a new appointment and returns the persisted entity.
        /// </summary>
        Task<Appointment> BookAppointmentAsync(Appointment appointment);

        /// <summary>
        /// Marks an appointment as visited.
        /// </summary>
        Task<bool> MarkVisitedAsync(int appointmentId);
    }
}



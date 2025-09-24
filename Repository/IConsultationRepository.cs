using ConsoleAppCms2025.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// Data access operations for consultations.
    /// </summary>
    public interface IConsultationRepository
    {
        /// <summary>
        /// Adds a consultation for an appointment.
        /// </summary>
        Task<bool> AddConsultationAsync(int appointmentId, string symptoms, string diagnosis, string notes);
    }
}
 

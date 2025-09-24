using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Exposes operations for storing consultation details.
    /// </summary>
    public interface IConsultationService
    {
        /// <summary>
        /// Adds a consultation record for an appointment.
        /// </summary>
        Task<bool> AddConsultationAsync(int appointmentId, string symptoms, string diagnosis, string notes);
    }
}
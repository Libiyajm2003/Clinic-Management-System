using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Exposes doctor lookup operations.
    /// </summary>
    public interface IDoctorService
    {
        /// <summary>
        /// Returns all active doctors.
        /// </summary>
        Task<List<Doctor>> GetAllDoctorsAsync();

        /// <summary>
        /// Returns a doctor by identifier or null if not found.
        /// </summary>
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
    }
}


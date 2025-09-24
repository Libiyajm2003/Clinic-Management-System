using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// Data access operations for doctors.
    /// </summary>
    public interface IDoctorRepository
    {
        /// <summary>
        /// Returns all doctors from the data store.
        /// </summary>
        Task<List<Doctor>> GetAllDoctorsAsync();

        /// <summary>
        /// Retrieves a doctor by identifier.
        /// </summary>
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
    }
}


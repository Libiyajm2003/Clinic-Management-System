using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Exposes patient-related operations.
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Retrieves a patient by MMR number.
        /// </summary>
        Task<Patient> GetPatientByMMRAsync(string mmrNumber);

        /// <summary>
        /// Returns patients matching a phone number.
        /// </summary>
        Task<List<Patient>> GetPatientsByPhoneAsync(string phone);

        /// <summary>
        /// Adds a new patient.
        /// </summary>
        Task<int> AddPatientAsync(Patient patient);

        /// <summary>
        /// Generates the next MMR number.
        /// </summary>
        Task<string> GenerateNextMMRNumberAsync();
    }
}

using ConsoleAppCms2025.Model;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// Data access operations for medicine prescriptions.
    /// </summary>
    public interface IPrescriptionRepository
    {
        /// <summary>
        /// Adds a prescription row for an appointment using medicine name lookup.
        /// </summary>
        Task AddPrescriptionAsync(MedicinePrescription prescription);
    }
}

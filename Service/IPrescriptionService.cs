using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Exposes operations for adding medicine prescriptions.
    /// </summary>
    public interface IPrescriptionService
    {
        /// <summary>
        /// Adds a prescription using medicine name lookup, dosage, duration and quantity.
        /// </summary>
        Task AddPrescriptionAsync(int appointmentId, string medicineName, string dosage, string duration, int qty = 1);
    }
}

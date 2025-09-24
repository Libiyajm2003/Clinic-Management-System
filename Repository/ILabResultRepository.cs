using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// Data access operations for lab results/prescriptions.
    /// </summary>
    public interface ILabResultRepository
    {
        /// <summary>
        /// Adds a lab result entry for an appointment.
        /// </summary>
        Task AddLabResultAsync(int appointmentId, string labTestName, string labTestValue, string remarks);
    }
}

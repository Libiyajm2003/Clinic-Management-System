using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Exposes operations for lab results/prescriptions.
    /// </summary>
    public interface ILabResultService
    {
        /// <summary>
        /// Adds a lab result linked to an appointment.
        /// </summary>
        Task AddLabResultAsync(int appointmentId, string labTestName, string labTestValue, string remarks);
    }
}

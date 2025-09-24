using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Exposes billing related operations.
    /// </summary>
    public interface IBillingService
    {
        /// <summary>
        /// Retrieves billing by appointment identifier.
        /// </summary>
        Task<Billing> GetBillingByAppointmentIdAsync(int appointmentId);

        /// <summary>
        /// Gets all billings for a patient MMR number.
        /// </summary>
        Task<List<Billing>> GetBillingsByPatientMMRAsync(string patientMMR);

        /// <summary>
        /// Adds a billing entry.
        /// </summary>
        Task<int> AddBillingAsync(Billing bill);

        /// <summary>
        /// Computes charges and persists a completed bill.
        /// </summary>
        Task<int> GenerateBillAsync(Billing bill);
    }
}


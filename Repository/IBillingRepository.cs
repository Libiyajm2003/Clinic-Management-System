using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// Data access operations for billing.
    /// </summary>
    public interface IBillingRepository
    {
        /// <summary>
        /// Retrieves billing for a given appointment identifier.
        /// </summary>
        Task<Billing> GetBillingByAppointmentIdAsync(int appointmentId);

        /// <summary>
        /// Gets all bills for a patient by MMR number.
        /// </summary>
        Task<List<Billing>> GetBillingsByPatientMMRAsync(string patientMMR);

        /// <summary>
        /// Inserts a new billing record.
        /// </summary>
        Task<int> AddBillingAsync(Billing bill);

        /// <summary>
        /// Calculates total medicine charges for an appointment.
        /// </summary>
        Task<decimal> GetMedicineChargesByAppointmentAsync(int appointmentId);

        /// <summary>
        /// Calculates total lab charges for an appointment.
        /// </summary>
        Task<decimal> GetLabChargesByAppointmentAsync(int appointmentId);
    }
}

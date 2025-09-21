using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    public interface IBillingRepository
    {
        Task<Billing> GetBillingByAppointmentIdAsync(int appointmentId);
        Task<List<Billing>> GetBillingsByPatientMMRAsync(string patientMMR);
        Task<int> AddBillingAsync(Billing bill);

        // Add these two methods to fix the error
        Task<decimal> GetMedicineChargesByAppointmentAsync(int appointmentId);
        Task<decimal> GetLabChargesByAppointmentAsync(int appointmentId);
    }
}

using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public interface IBillingService
    {
        Task<Billing> GetBillingByAppointmentIdAsync(int appointmentId);
        Task<List<Billing>> GetBillingsByPatientMMRAsync(string patientMMR);
        Task<int> AddBillingAsync(Billing bill);

        // New method to auto-calculate charges
        Task<int> GenerateBillAsync(Billing bill);
    }
}


using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Implements billing-related operations and coordinates charge calculations.
    /// </summary>
    public class BillingServiceImpl : IBillingService
    {
        private readonly IBillingRepository _billingRepo;

        /// <summary>
        /// Creates a new instance of <see cref="BillingServiceImpl"/>.
        /// </summary>
        public BillingServiceImpl(IBillingRepository billingRepo)
        {
            _billingRepo = billingRepo;
        }

        /// <inheritdoc />
        public async Task<int> AddBillingAsync(Billing bill)
        {
            return await _billingRepo.AddBillingAsync(bill);
        }

        /// <inheritdoc />
        public async Task<Billing> GetBillingByAppointmentIdAsync(int appointmentId)
        {
            return await _billingRepo.GetBillingByAppointmentIdAsync(appointmentId);
        }

        /// <inheritdoc />
        public async Task<List<Billing>> GetBillingsByPatientMMRAsync(string patientMMR)
        {
            return await _billingRepo.GetBillingsByPatientMMRAsync(patientMMR);
        }

        /// <inheritdoc />
        public async Task<int> GenerateBillAsync(Billing bill)
        {
            bill.MedicineCharges = await _billingRepo.GetMedicineChargesByAppointmentAsync(bill.AppointmentId);
            bill.LabCharges = await _billingRepo.GetLabChargesByAppointmentAsync(bill.AppointmentId);
            bill.TotalAmount = bill.ConsultationFee + bill.MedicineCharges + bill.LabCharges;

            bill.BillDate = System.DateTime.Now;
            bill.IsPaid = false;

            return await _billingRepo.AddBillingAsync(bill);
        }
    }
}

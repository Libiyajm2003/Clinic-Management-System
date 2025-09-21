using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public class BillingServiceImpl : IBillingService
    {
        private readonly IBillingRepository _billingRepo;

        public BillingServiceImpl(IBillingRepository billingRepo)
        {
            _billingRepo = billingRepo;
        }

        public async Task<int> AddBillingAsync(Billing bill)
        {
            return await _billingRepo.AddBillingAsync(bill);
        }

        public async Task<Billing> GetBillingByAppointmentIdAsync(int appointmentId)
        {
            return await _billingRepo.GetBillingByAppointmentIdAsync(appointmentId);
        }

        public async Task<List<Billing>> GetBillingsByPatientMMRAsync(string patientMMR)
        {
            return await _billingRepo.GetBillingsByPatientMMRAsync(patientMMR);
        }

        // Auto-calculate medicine & lab charges
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

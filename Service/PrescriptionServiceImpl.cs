using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public class PrescriptionServiceImpl : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepo;

        public PrescriptionServiceImpl(IPrescriptionRepository prescriptionRepo)
        {
            _prescriptionRepo = prescriptionRepo;
        }

        // Updated method to use MedicineName instead of MedicineId
        public async Task AddPrescriptionAsync(int appointmentId, string medicineName, string dosage, string duration, int qty = 1)
        {
            MedicinePrescription prescription = new MedicinePrescription
            {
                AppointmentId = appointmentId,
                MedicineName = medicineName,
                Dosage = dosage,
                Duration = duration,
                Qty = qty,
                IsActive = true
            };

            await _prescriptionRepo.AddPrescriptionAsync(prescription);
        }
    }
}

using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Implements prescription operations by delegating to repository.
    /// </summary>
    public class PrescriptionServiceImpl : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepo;

        /// <summary>
        /// Creates a new instance of <see cref="PrescriptionServiceImpl"/>.
        /// </summary>
        public PrescriptionServiceImpl(IPrescriptionRepository prescriptionRepo)
        {
            _prescriptionRepo = prescriptionRepo;
        }

        /// <inheritdoc />
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

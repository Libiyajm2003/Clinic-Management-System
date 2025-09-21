using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public interface IPrescriptionService
    {
        // Use MedicineName instead of MedicineId, and include Duration and Qty
        Task AddPrescriptionAsync(int appointmentId, string medicineName, string dosage, string duration, int qty = 1);
    }
}

using ConsoleAppCms2025.Model;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    public interface IPrescriptionRepository
    {
        Task AddPrescriptionAsync(MedicinePrescription prescription);
    }
}

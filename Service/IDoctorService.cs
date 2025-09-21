using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
    }
}


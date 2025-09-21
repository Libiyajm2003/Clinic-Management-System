using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
    }
}


using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public class DoctorServiceImpl : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;

        public DoctorServiceImpl(IDoctorRepository doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepo.GetAllDoctorsAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
        {
            return await _doctorRepo.GetDoctorByIdAsync(doctorId);
        }
    }
}

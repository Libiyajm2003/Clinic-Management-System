using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Implements doctor-related read operations.
    /// </summary>
    public class DoctorServiceImpl : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;

        /// <summary>
        /// Creates a new instance of <see cref="DoctorServiceImpl"/>.
        /// </summary>
        public DoctorServiceImpl(IDoctorRepository doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        /// <inheritdoc />
        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepo.GetAllDoctorsAsync();
        }

        /// <inheritdoc />
        public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
        {
            return await _doctorRepo.GetDoctorByIdAsync(doctorId);
        }
    }
}

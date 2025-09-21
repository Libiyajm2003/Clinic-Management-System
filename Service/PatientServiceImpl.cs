using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public class PatientServiceImpl : IPatientService
    {
        private readonly IPatientRepository _repo;

        public PatientServiceImpl(IPatientRepository repo)
        {
            _repo = repo;
        }

        public Task<Patient> GetPatientByMMRAsync(string mmr)
            => _repo.GetPatientByMMRAsync(mmr);

        public Task<List<Patient>> GetPatientsByPhoneAsync(string phone)   // ✅ updated
            => _repo.GetPatientsByPhoneAsync(phone);

        public Task<int> AddPatientAsync(Patient patient)
            => _repo.AddPatientAsync(patient);

        public Task<string> GenerateNextMMRNumberAsync()
            => _repo.GenerateNextMMRNumberAsync();
    }
}

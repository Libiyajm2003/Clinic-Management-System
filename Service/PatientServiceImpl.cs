using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    /// <summary>
    /// Implements patient-related operations by delegating to repository.
    /// </summary>
    public class PatientServiceImpl : IPatientService
    {
        private readonly IPatientRepository _repo;

        /// <summary>
        /// Creates a new instance of <see cref="PatientServiceImpl"/>.
        /// </summary>
        public PatientServiceImpl(IPatientRepository repo)
        {
            _repo = repo;
        }

        /// <inheritdoc />
        public Task<Patient> GetPatientByMMRAsync(string mmr)
            => _repo.GetPatientByMMRAsync(mmr);

        /// <inheritdoc />
        public Task<List<Patient>> GetPatientsByPhoneAsync(string phone)
            => _repo.GetPatientsByPhoneAsync(phone);

        /// <inheritdoc />
        public Task<int> AddPatientAsync(Patient patient)
            => _repo.AddPatientAsync(patient);

        /// <inheritdoc />
        public Task<string> GenerateNextMMRNumberAsync()
            => _repo.GenerateNextMMRNumberAsync();
    }
}

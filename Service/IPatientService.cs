using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Service
{
    public interface IPatientService
    {
        Task<Patient> GetPatientByMMRAsync(string mmrNumber);
        Task<List<Patient>> GetPatientsByPhoneAsync(string phone); // ✅ list, not single
        Task<int> AddPatientAsync(Patient patient);
        Task<string> GenerateNextMMRNumberAsync();
    }
}

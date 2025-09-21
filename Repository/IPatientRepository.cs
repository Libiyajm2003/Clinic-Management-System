using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPatientRepository
{
    Task<Patient> GetPatientByMMRAsync(string mmrNumber);
    Task<List<Patient>> GetPatientsByPhoneAsync(string phone);  // ✅ return list
    Task<int> AddPatientAsync(Patient patient);
    Task<string> GenerateNextMMRNumberAsync();
}


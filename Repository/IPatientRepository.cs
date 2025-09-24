using ConsoleAppCms2025.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPatientRepository
{
    /// <summary>
    /// Retrieves a patient by MMR number.
    /// </summary>
    Task<Patient> GetPatientByMMRAsync(string mmrNumber);

    /// <summary>
    /// Returns all patients matching the specified phone number.
    /// </summary>
    Task<List<Patient>> GetPatientsByPhoneAsync(string phone);

    /// <summary>
    /// Inserts a new patient row.
    /// </summary>
    Task<int> AddPatientAsync(Patient patient);

    /// <summary>
    /// Generates the next sequential MMR number.
    /// </summary>
    Task<string> GenerateNextMMRNumberAsync();
}


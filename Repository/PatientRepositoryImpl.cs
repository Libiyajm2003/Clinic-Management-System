using ConsoleAppCms2025.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// SQL Server implementation for patient persistence and queries.
    /// </summary>
    public class PatientRepositoryImpl : IPatientRepository
    {
        private readonly string _connectionString;

        public PatientRepositoryImpl()
        {
            _connectionString = @"Server=localhost;Database=cmsv2025db;Trusted_Connection=True;Encrypt=False;";
        }

        /// <inheritdoc />
        public async Task<Patient> GetPatientByMMRAsync(string mmr)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = @"SELECT MMRNumber, FullName, Gender, Age, Phone, Address, MembershipId, IsActive 
                             FROM TblPatient WHERE MMRNumber=@mmr";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@mmr", mmr);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Patient
                {
                    MMRNumber = reader["MMRNumber"].ToString(),
                    FullName = reader["FullName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Phone = reader["Phone"].ToString(),
                    Address = reader["Address"].ToString(),
                    MembershipId = reader["MembershipId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MembershipId"]),
                    IsActive = (bool)reader["IsActive"]
                };
            }
            return null;
        }

        /// <inheritdoc />
        public async Task<List<Patient>> GetPatientsByPhoneAsync(string phone)
        {
            var patients = new List<Patient>();

            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = @"SELECT MMRNumber, FullName, Gender, Age, Phone, Address, MembershipId, IsActive 
                     FROM TblPatient WHERE Phone=@phone";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@phone", phone);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())   // ✅ loop through all rows
            {
                patients.Add(new Patient
                {
                    MMRNumber = reader["MMRNumber"].ToString(),
                    FullName = reader["FullName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Age = Convert.ToInt32(reader["Age"]),
                    Phone = reader["Phone"].ToString(),
                    Address = reader["Address"].ToString(),
                    MembershipId = reader["MembershipId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MembershipId"]),
                    IsActive = (bool)reader["IsActive"]
                });
            }

            return patients;
        }


        /// <inheritdoc />
        public async Task<int> AddPatientAsync(Patient patient)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = @"INSERT INTO TblPatient(MMRNumber, FullName, Gender, Age, DOB, Phone, Address, MembershipId, IsActive) 
                     VALUES(@mmr, @fullName, @gender, @age, @dob, @phone, @address, @membershipId, @isActive)";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@mmr", patient.MMRNumber);
            cmd.Parameters.AddWithValue("@fullName", patient.FullName);
            cmd.Parameters.AddWithValue("@gender", patient.Gender);
            cmd.Parameters.AddWithValue("@age", patient.Age);
            cmd.Parameters.AddWithValue("@dob", patient.DOB);
            cmd.Parameters.AddWithValue("@phone", patient.Phone);
            cmd.Parameters.AddWithValue("@address", patient.Address ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@membershipId", patient.MembershipId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@isActive", patient.IsActive);

            return await cmd.ExecuteNonQueryAsync();
        }

        /// <inheritdoc />
        public async Task<string> GenerateNextMMRNumberAsync()
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = "SELECT MAX(MMRNumber) FROM TblPatient";
            using SqlCommand cmd = new SqlCommand(query, con);
            var result = await cmd.ExecuteScalarAsync();

            if (result == DBNull.Value || result == null)
                return "MMR1001";

            string maxMMR = result.ToString();
            int number = int.Parse(maxMMR.Substring(3)) + 1;
            return "MMR" + number;
        }

        /// <summary>
        /// Updates patient fields. Reserved for future use.
        /// </summary>
        public async Task<int> UpdatePatientAsync(Patient patient)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            string query = @"UPDATE TblPatient SET FullName=@fullName, Gender=@gender, Age=@age, Phone=@phone, 
                             Address=@address, MembershipId=@membershipId, IsActive=@isActive
                             WHERE MMRNumber=@mmr";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@mmr", patient.MMRNumber);
            cmd.Parameters.AddWithValue("@fullName", patient.FullName);
            cmd.Parameters.AddWithValue("@gender", patient.Gender);
            cmd.Parameters.AddWithValue("@age", patient.Age);
            cmd.Parameters.AddWithValue("@phone", patient.Phone);
            cmd.Parameters.AddWithValue("@address", patient.Address ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@membershipId", patient.MembershipId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@isActive", patient.IsActive);

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}


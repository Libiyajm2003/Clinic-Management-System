using ConsoleAppCms2025.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025.Repository
{
    /// <summary>
    /// SQL Server implementation of doctor queries.
    /// </summary>
    public class DoctorRepositoryImpl : IDoctorRepository
    {
        private readonly string connectionString = @"Data Source=.;Initial Catalog=cmsv2025db;Integrated Security=True";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                Encrypt = false
            };
            return new SqlConnection(builder.ConnectionString);
        }

        /// <inheritdoc />
        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            List<Doctor> doctors = new();
            using SqlConnection con = GetConnection();
            string query = @"SELECT d.DoctorId, u.FullName, s.SpecializationName, d.ConsultationFee
                             FROM TblDoctor d
                             JOIN TblUser u ON d.UserId = u.UserId
                             JOIN TblSpecialization s ON d.SpecializationId = s.SpecializationId
                             WHERE d.IsActive = 1";

            using SqlCommand cmd = new SqlCommand(query, con);
            await con.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                doctors.Add(new Doctor
                {
                    DoctorId = (int)reader["DoctorId"],
                    DoctorName = reader["FullName"].ToString(),
                    SpecializationName = reader["SpecializationName"].ToString(),
                    ConsultationFee = (decimal)reader["ConsultationFee"]
                });
            }
            return doctors;
        }

        /// <inheritdoc />
        public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
        {
            using SqlConnection con = GetConnection();
            string query = @"SELECT d.DoctorId, u.FullName, s.SpecializationName, d.ConsultationFee
                             FROM TblDoctor d
                             JOIN TblUser u ON d.UserId = u.UserId
                             JOIN TblSpecialization s ON d.SpecializationId = s.SpecializationId
                             WHERE d.DoctorId = @DoctorId";

            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@DoctorId", doctorId);
            await con.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Doctor
                {
                    DoctorId = (int)reader["DoctorId"],
                    DoctorName = reader["FullName"].ToString(),
                    SpecializationName = reader["SpecializationName"].ToString(),
                    ConsultationFee = (decimal)reader["ConsultationFee"]
                };
            }
            return null;
        }
    }
}
